using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace chit_chat_api.Models
{
    public class Message
    {
        [Key]
        public int id { get; set; }
        public string? sender_id { get; set; }
        public string? receiver_id { get; set; }
        public string ? message { get; set; }
        public bool ? is_read { get; set; }
        public User? User { get; set; }

    }
}
