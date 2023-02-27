using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MtchMKRAPI.Data.Entities
{
    public class MatchRequestDetail
    {
        
        public Guid MatchRequestDetailID { get; set; }
        public Guid MatchId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid RequestedUserId { get; set; }
        public bool isAgreed { get; set; }
    }
        
}
