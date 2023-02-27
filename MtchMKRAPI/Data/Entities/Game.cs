using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MtchMKRAPI.Data.Entities
{
    public class Game
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GameId { get; set; } 

        public string Name { get; set; }

        public int MaxFrame { get; set; }

        public DateTime CreatedDate { get; set; }

        //public Game()
        //{
        //    Matches = new HashSet<Match>();
        //}

        //public Guid GameId { get; set; }
        //public string Name { get; set; } = null!;
        //public DateTime CreatedDate { get; set; }

        //public virtual ICollection<Match> Matches { get; set; }
    }
}
