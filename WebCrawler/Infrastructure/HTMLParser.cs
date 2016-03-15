namespace WebCrawler.Infrastructure
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Interfaces;
    using Utilities;

    public class HTMLParser : IHTMLParser
    {
        public IList<string> GetImageSources(string html)
        {
            IList<string> imageUrls = this.GetUrl(html);

            return imageUrls;
        }

        public IList<string> GetAnchorUrl(string html)
        {

            IList<string> anchorTagsList = this.GetUrl(html);

            return anchorTagsList;
        }

        private IList<string> GetUrl(string html)
        {
            string urlPattern = Constants.UrlPattern;
            Regex regex = new Regex(urlPattern);
            List<string> tagsList = new List<string>();

            MatchCollection matches = regex.Matches(html);
            foreach (Match match in matches)
            {
                string url = match.Groups[1].Value;
                tagsList.Add(url);
            }

            return tagsList;
        }
    }
}
