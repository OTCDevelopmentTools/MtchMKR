using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MtchMKRAPI.Data.Entities
{
    public class UserDeviceInfo
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UsersDeviceInfoId { get; set; }
        public string DeviceId { get; set; }
        public Guid UserId { get; set; }
        public string DeviceToken { get; set; }
        public string DeviceType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
