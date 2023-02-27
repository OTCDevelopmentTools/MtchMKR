using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MtchMKRAPI.Data.Entities
{
    public class UserImage
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserImageId { get; set; }
        public Guid UserId { get; set; }
        public string? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }

        public string? ImageExtension { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
