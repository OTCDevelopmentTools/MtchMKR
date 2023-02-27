using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MtchMKRAPI.Data.Entities
{
    public class User
    {
        //public User()
        //{
        //    MatchBookings = new HashSet<MatchBooking>();
        //    Profiles = new HashSet<Profile>();
        //    UserImages = new HashSet<UserImage>();
        //}
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public static explicit operator Guid(User v)
        {
            throw new NotImplementedException();
        }

        //public virtual ICollection<MatchBooking>? MatchBookings { get; set; }
        //public virtual ICollection<Profile>? Profiles { get; set; }
        //public virtual ICollection<UserImage>? UserImages { get; set; }

    }
}
