namespace WebCrawler.Interfaces
{
    using System.Collections.Generic;

    public interface IHTMLParser : IParser
    {
        IList<string> GetImageSources(string html);

        IList<string> GetAnchorUrl(string html);
    }
}
