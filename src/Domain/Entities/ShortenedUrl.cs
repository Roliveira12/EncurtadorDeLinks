namespace Domain.Entities
{
    public record ShortenedUrl
    {
        public long Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShorterUrlId { get; set; }
        public long AccessCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}