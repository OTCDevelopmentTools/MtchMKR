namespace MtchMKRAPI.Data.Entities
{
    public class Match
    {

        public Guid MatchId { get; set; }
        public Guid GameId { get; set; }
        public DateTime Date { get; set; }
        public Guid LocationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid RequestedToUser { get; set; }
        public Guid CreatedByUser { get; set; }
        public bool isAgreed { get; set; }



        //public Match()
        //{
        //    MatchBookings = new HashSet<MatchBooking>();
        //}

        //public Guid MatchId { get; set; }
        //public Guid GameId { get; set; }
        //public string Frames { get; set; } = null!;
        //public DateTime Date { get; set; }
        //public TimeSpan Time { get; set; }
        //public DateTime CreatedDate { get; set; }

        //public virtual Game Game { get; set; } = null!;
        //public virtual ICollection<MatchBooking> MatchBookings { get; set; }

    }
}
