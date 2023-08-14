namespace Application.UseCases.Boundaries.ShortenerUrl
{
    public class ShortenerUrlOutput
    {
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public string Id { get; set; }
        public long Hits { get; set; }
    }
}