using System.ComponentModel.DataAnnotations;

namespace chit_chat_api.Models
{
    public class OnlineUsers
    {
        [Key]
        public int user_id {  get; set; }
        public int connection_id {  get; set; }
        public string? username { get; set; }
        public string? full_name {  get; set; }
        public string? profile_image {  get; set; }
        public bool is_online {  get; set; }
        public int unread_count {  get; set; }
        public DateTime created_at { get; set; }
    }
}
