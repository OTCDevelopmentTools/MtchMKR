using System;

namespace MtchMKRAPI.Data.Entities
{
    public class UsersPaymentInfo
    {
            public Guid UsersPaymentInfoId { get; set; }
            public string TransactionId { get; set; }
            public Guid UserId { get; set; }
            public decimal Payment { get; set; }
            public int MatchCount { get; set; }
            public DateTime CreatedDate { get; set; }
    }
}
