using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MtchMKRAPI.Data.Entities
{
    public class MatchCreation
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MatchId { get; set; }
        public Guid GameId { get; set; }
        public DateTime MatchDate { get; set; }
        public Guid LocationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Guid> ?matchRequestUsers { get; set; }
        public Guid CreatedByUser { get; set; }
        
    }
}
  