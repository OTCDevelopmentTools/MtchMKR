namespace MtchMKRAPI.Data.Entities
{
    public class PlaylistForSearch

    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }

        public Guid GameId { get; set; }
        public string? GameName { get; set; }

        public string? Place { get; set; }
        public int? Rating { get; set; }

        public int? GameFrame { get; set; }
        public byte[]? ImageData { get; set; }
    }
}
