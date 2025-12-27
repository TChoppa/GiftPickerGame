using System.ComponentModel.DataAnnotations;

namespace SantaGift.Models
{
    public class Register
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
    }
}
 