namespace MtchMKRAPI.Data.Entities
{
    public class PendingMatchDetail
    {

        public Guid UserID { get; set; }
        public Guid RequestedToUser { get; set; }
        public string? Name { get; set; }
                
        public byte[]? ImageData { get; set; }
                
        public string? GameName { get; set; }

        public Guid CreatedByUser { get; set; }

        public Guid MatchId { get; set; }
        
        public DateTime MatchDate { get; set; }

        public string? Location { get; set; }




    }
}
