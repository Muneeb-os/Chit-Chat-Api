using System.ComponentModel.DataAnnotations;

namespace chit_chat_api.Models
{
    public class Message
    {
        [Key]
        public int id { get; set; }
    }
}
