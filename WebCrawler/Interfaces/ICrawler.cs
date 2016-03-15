namespace WebCrawler.Interfaces
{
    public interface ICrawler
    {
        IDatabase Database { get; }

        int SearchingDepth { get; set; }

        void Crawl(string url);
    }
}
