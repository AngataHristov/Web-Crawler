namespace WebCrawler.Utilities
{
    public static class Constants
    {
        public const int MaxCrawlingDepth = 3;

        public const string EnterUrlMsg = "Enter URL: ";
        public const string EnterDepthMsg = "Enter depth(max 3): ";
        public const string UrlPattern = @"<img.+?src=\""(.+?)\"".*?>";
    }
}
