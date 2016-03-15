namespace WebCrawler.Core
{
    using System.Collections.Generic;

    using Interfaces;

    public class Database : IDatabase
    {
        private readonly IList<string> imagesUrls;
        private readonly IList<string> anchorUrls;

        public Database()
        {
            this.imagesUrls = new List<string>();
            this.anchorUrls = new List<string>();
        }

        public IEnumerable<string> ImagesUrls
        {
            get { return this.imagesUrls; }
        }

        public IEnumerable<string> AnchorUrls
        {
            get { return this.anchorUrls; }
        }

        public void AddImagesUrls(string imagesUrls)
        {
            this.imagesUrls.Add(imagesUrls);
        }

        public void AddAnchorUrls(string anchorUrls)
        {
            this.anchorUrls.Add(anchorUrls);
        }
    }
}
