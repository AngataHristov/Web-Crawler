namespace WebCrawler.Interfaces
{
    using System.Collections.Generic;

    public interface IDatabase
    {
        IEnumerable<string> ImagesUrls { get; }

        IEnumerable<string> AnchorUrls { get; }

        void AddImagesUrls(string imagesUrls);

        void AddAnchorUrls(string anchorUrls);
    }
}
