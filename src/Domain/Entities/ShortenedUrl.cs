namespace Domain.Entities
{
    public record ShortenedUrl
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShorterUrlId { get; set; }
        public int AccessCount { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}