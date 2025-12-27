using System.ComponentModel.DataAnnotations;

namespace SantaGift.Models
{
    public class Participants
    {
        [Key]
        public int Id { get; set; }      
        public string Name { get; set; } = string.Empty;
        public string UseName { get; set; } = string.Empty;
    }
}
