using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MtchMKRAPI.Data.Entities
{
    public class MatchBooking
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MatchBookingId { get; set; }
        public Guid UserId { get; set; }
        public Guid MatchId { get; set; }
        public DateTime CreatedDate { get; set; }


        //public partial class MatchBooking
        //{
        //    public Guid MatchBookingId { get; set; }
        //    public Guid MatchId { get; set; }
        //    public Guid UserId { get; set; }
        //    public bool Scorer { get; set; }
        //    public DateTime CreatedDate { get; set; }

        //    public virtual Match Match { get; set; } = null!;
        //    public virtual User User { get; set; } = null!;
        //}
    }
}
