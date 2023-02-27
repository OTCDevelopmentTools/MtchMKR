namespace MtchMKRAPI.Data.Entities
{
    public class UserProfileF
    {


        public Guid UserId { get; set; }
        public string? UserName { get; set; }

        public Guid GameId { get; set; }
        public string? GameName { get; set; }

        public string? Place { get; set; }
        public int? Rating { get; set; }

        public int? GameFrame { get; set; }
        public byte[]? ImageData { get; set; }

    }
}
