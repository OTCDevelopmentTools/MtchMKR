namespace MtchMKRAPI.Data.Entities
{
    public class MatchScoreCard
    {
        public Guid MatchScoreCardId { get; set; }
        public Guid MatchId { get; set; }
        public int FrameTotal { get; set; }
        public int FrameNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid FrameWinner { get; set; }
        public bool ConfirmedByPlayer { get; set; }
    }
}
