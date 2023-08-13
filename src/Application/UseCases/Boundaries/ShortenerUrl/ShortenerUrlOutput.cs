namespace Application.UseCases.Boundaries.ShortenerUrl
{
    public class ShortenerUrlOutput
    {
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public long Id { get; set; }
        public long AcessCount { get; set; }
    }
}