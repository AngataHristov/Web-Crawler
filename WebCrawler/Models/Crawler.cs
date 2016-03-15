namespace WebCrawler.Models
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Interfaces;
    using Utilities;

    public abstract class Crawler : ICrawler
    {
        private readonly IHTMLParser htmlParser;
        private readonly IDatabase database;
        private readonly IOutputWriter writer;
        private readonly WebClient client;

        protected Crawler(IDatabase database, IHTMLParser htmlParser, IOutputWriter writer)
        {
            this.htmlParser = htmlParser;
            this.database = database;
            this.writer = writer;
            this.DepthCounter = 0;
            this.client = new WebClient();
        }

        public IDatabase Database
        {
            get { return this.database; }
        }

        protected IOutputWriter Writer
        {
            get { return this.writer; }
        }

        protected IHTMLParser HtmlParser
        {
            get { return this.htmlParser; }
        }

        protected WebClient Client
        {
            get { return this.client; }
        }

        protected int DepthCounter { get; set; }

        public int SearchingDepth { get; set; }

        public abstract void Crawl(string url);

        protected void ValidateSearchingDepth()
        {
            if (this.SearchingDepth > Constants.MaxCrawlingDepth)
            {
                this.SearchingDepth = Constants.MaxCrawlingDepth;
            }
        }

        protected IList<string> FixUrl(IList<string> urls, string url)
        {
            IList<string> imagesUrls = urls;

            for (int i = 0; i < imagesUrls.Count; i++)
            {
                if (!imagesUrls[i].StartsWith("http"))
                {
                    imagesUrls[i] = url + imagesUrls[i];
                }
            }

            return imagesUrls;
        }
    }
}
