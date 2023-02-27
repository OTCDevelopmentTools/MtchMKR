namespace MtchMKRAPI.Data.Entities
{
    public class SearchPlayers
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

            public Guid GameId { get; set; }
            public string? GameName { get; set; }

            public int? MaxFrame { get; set; }

            public DateTime MatchDate { get; set; }

      //      public DateTime MatchTime { get; set; }

       
    }
}
