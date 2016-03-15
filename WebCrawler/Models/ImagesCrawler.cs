namespace WebCrawler.Models
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Interfaces;

    public class ImagesCrawler : Crawler
    {
        private static ICrawler instance;
        private static object syncLock = new object();

        private ImagesCrawler(IDatabase database, IHTMLParser htmlParser, IOutputWriter writer)
            : base(database, htmlParser, writer)
        {
        }

        public static ICrawler GetInstance(IDatabase database, IHTMLParser htmlParser, IOutputWriter writer)
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        return new ImagesCrawler(database, htmlParser, writer);
                    }
                }
            }

            return instance;
        }

        public override void Crawl(string url)
        {
            string html = string.Empty;

            this.ValidateSearchingDepth();

            using (this.Client)
            {
                html = this.DownloadHtml(url);

                IList<string> imagesUrls = this.HtmlParser.GetImageSources(html);

                url = url.TrimEnd('/');

                imagesUrls = this.FixUrl(imagesUrls, url);

                foreach (string imagesUrl in imagesUrls)
                {
                    int lastIndexOfForwardSlash = imagesUrl.LastIndexOf('/');

                    string fileName = imagesUrl.Substring(lastIndexOfForwardSlash + 1);
                    fileName = "images/" + fileName;

                    this.DownloadFile(imagesUrl, fileName);

                    this.DepthCounter++;

                    if (this.DepthCounter <= this.SearchingDepth)
                    {
                        this.DownloadInDepth(html, url);
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("{0} crawled!", url);
                    Console.ForegroundColor = ConsoleColor.Black;
                }
            }
        }

        private string DownloadHtml(string url)
        {
            string html = string.Empty;

            try
            {
                html = this.Client.DownloadString(url);
            }
            catch (WebException wex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                this.Writer.WriteLine(wex.Message);
                Console.ForegroundColor = ConsoleColor.Black;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Black;
            }

            return html;
        }

        private void DownloadFile(string imagesUrl, string fileName)
        {
            try
            {
                this.Client.DownloadFile(imagesUrl, fileName);

                Console.ForegroundColor = ConsoleColor.Green;
                this.Writer.WriteLine(imagesUrl);
                Console.ForegroundColor = ConsoleColor.Black;
            }
            catch (WebException webEx)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                this.Writer.WriteLine(webEx.Message);
                Console.ForegroundColor = ConsoleColor.Black;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                this.Writer.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }

        private void DownloadInDepth(string html, string url)
        {
            IList<string> anchorUrls = this.HtmlParser.GetAnchorUrl(html);

           url= url.TrimEnd('/');

            for (int i = 0; i < anchorUrls.Count; i++)
            {
                if (!anchorUrls[i].StartsWith("http"))
                {
                    anchorUrls[i] = url + anchorUrls[i];
                }
            }

            Parallel.ForEach(anchorUrls, this.Crawl);
        }
    }
}
