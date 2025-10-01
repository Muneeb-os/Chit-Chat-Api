using chit_chat_api.DB_Data;
using chit_chat_api.Models;
using Microsoft.AspNetCore.SignalR;
using System;

namespace chit_chat_api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly _dbContext _context;

        public ChatHub(_dbContext context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            var user_id = Context.User?.Identity?.Name;
            var connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(user_id))
            {
                var onlineUser = new OnlineUsers
                {
                    user_id = int.Parse(user_id),
                    connection_id = connectionId.GetHashCode(),
                    is_online = true,
                    created_at = DateTime.Now
                };

                _context.OnlineUsers.Add(onlineUser);
                await _context.SaveChangesAsync();

                await Clients.All.SendAsync("UserOnline", user_id);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var conn_id = Context.ConnectionId;
            var onlineUser = _context.OnlineUsers.FirstOrDefault(u => u.connection_id == conn_id.GetHashCode());

            if (onlineUser != null)
            {
                onlineUser.is_online = false;
                await _context.SaveChangesAsync();

                await Clients.All.SendAsync("UserOffline", onlineUser.user_id);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string sender_id, string receiver_id, string message_text)
        {
            var message = new Message
            {
                sender_id = sender_id,
                receiver_id = receiver_id,
                message = message_text,
                is_read = false,
                status = true,
                created_at = DateTime.Now
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            var receiverConnections = _context.OnlineUsers
                .Where(u => u.user_id.ToString() == receiver_id && u.is_online)
                .Select(u => u.connection_id.ToString());

            foreach (var conn in receiverConnections)
            {
                await Clients.Client(conn).SendAsync("ReceiveMessage", message);
            }

            await Clients.Caller.SendAsync("ReceiveMessage", message);
        }
    }
}
