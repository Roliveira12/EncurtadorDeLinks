namespace Domain.Entities
{
    public record ShortenedUrl
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public long Hits { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}