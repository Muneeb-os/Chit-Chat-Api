using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace chit_chat_api.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string? user_name {  get; set; }
        public string? user_email { get; set; }
        public string? user_password { get; set; }
        public DateTime? created_at { get; set; }
        public User_Profile_Image? User_Profile_Image { get; set; }
    }
}
