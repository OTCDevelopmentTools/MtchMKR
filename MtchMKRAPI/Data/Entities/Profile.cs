using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MtchMKRAPI.Data.Entities
{
    public class Profile
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProfileId { get; set; }
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public string?  Place { get; set; }

        public decimal? RadiusLocator { get; set; }
        public string? RegularAvailability { get; set; }
     // 

        public string? Preferredgames { get; set; }

        public int? Rating { get; set; }

        public string? PlaysAgainst { get; set; }
        public string? NotificationMethod { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
