namespace WebCrawler.Core
{
    using System;
    using System.Threading.Tasks;
    using Interfaces;
    using Utilities;

    public class Engine : IEngine
    {
        private readonly ICrawler imageCrawler;
        private readonly IInputReader reader;
        private readonly IOutputWriter writer;

        public Engine(ICrawler imageCrawler, IInputReader reader, IOutputWriter writer)
        {
            this.imageCrawler = imageCrawler;
            this.reader = reader;
            this.writer = writer;
        }

        public IInputReader Reader
        {
            get { return this.reader; }
        }

        public IOutputWriter Writer
        {
            get { return this.writer; }
        }

        public ICrawler ImageCrawler
        {
            get { return this.imageCrawler; }
        }

        public void Run()
        {
            this.writer.WriteLine(Constants.EnterUrlMsg);
            string url = this.reader.ReadNextLine();

            this.writer.Write(Constants.EnterDepthMsg);
            int searchingDepth = int.Parse(this.reader.ReadNextLine());

            this.imageCrawler.SearchingDepth = searchingDepth;

            while (url != string.Empty)
            {
                try
                {
                    this.imageCrawler.Crawl(url);
                    this.CrawlAsync(url);
                }
                catch (Exception ex)
                {
                    this.writer.WriteLine(ex.Message);
                }

                url = this.reader.ReadNextLine();
            }
        }

        private async void CrawlAsync(string url)
        {
            await Task.Run(() => this.imageCrawler.Crawl(url));
        }
    }
}
