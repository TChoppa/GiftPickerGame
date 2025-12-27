using System.ComponentModel.DataAnnotations;

namespace SantaGift.Models
{
    public class GiftSponsers
    {
        [Key]
        public int Id { get; set; }
        public string Sponser { get; set; } = string.Empty;
        public string Picker { get; set; } = string.Empty;
        public string UseName { get; set; } = string.Empty;
        public bool isParticipant { get; set; }
    }
}
