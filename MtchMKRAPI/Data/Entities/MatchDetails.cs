namespace MtchMKRAPI.Data.Entities
{
    public class MatchDetails
    {
        public Guid MatchId { get; set; }

        public string? GameName { get; set; }
        public string? Frames { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string? Location { get; set; }
       
    }
}
