namespace Domain.Entities
{
    public class ShortUrlEvent
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShorterUrlId { get; set; }
    }
}