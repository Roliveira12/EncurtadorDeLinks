namespace WebApi.Boundaries.ShortenerUrl
{
    public class ShortenerUrlOutput
    {
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public string Id { get; set; }
        public int AcessCount { get; set; }
    }
}