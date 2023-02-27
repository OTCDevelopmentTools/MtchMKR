using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MtchMKRAPI.Data.Entities
{
    public class UserRegistration
    {
        [Key]
        [Required]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid UserRegistrationId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Telephone { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? imageData { get; set; }

    }
}
