namespace MtchMKRAPI.Data.Entities
{
    public class ShowCase
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? Telephone { get; set; }

        public string? Place { get; set; }
        public decimal? RadiusLocator { get; set; }
        public string? RegularAvailability { get; set; }
        public string? Preferredgames { get; set; }

        public int? Rating { get; set; }
        public string? PlaysAgainst { get; set; }
        public string? NotificationMethod { get; set; }
        public string? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }

        public string? ImageExtension { get; set; }
        public Guid? MatchId { get; set; }
        public string? GameName { get; set; }
        public int? MaxFrame { get; set; }

        public DateTime? Date { get; set; }
     
        public string? Location { get; set; }

        public Guid CreatedByUser   { get; set; }

        public Guid RequestedToUser { get; set; }

    }
}
