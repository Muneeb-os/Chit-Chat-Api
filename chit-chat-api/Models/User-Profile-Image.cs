using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chit_chat_api.Models
{
    public class User_Profile_Image
    {
        [Key]
        public int id { get; set; }
        public string? image_url {  get; set; }
        public DateTime? created_at { get; set; }
        [ForeignKey("User")]
        public int user_id {  get; set; }
        public User? User { get; set; }
    }
}
