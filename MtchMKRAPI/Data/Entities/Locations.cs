using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MtchMKRAPI.Data.Entities
{
    public class Locations
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LocationId { get; set; }

        public string? Location { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
