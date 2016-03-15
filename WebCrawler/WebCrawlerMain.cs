namespace WebCrawler
{
    using Core;
    using Core.IO;
    using Infrastructure;
    using Interfaces;
    using Models;

    public class WebCrawlerMain
    {
        public static void Main()
        {
            IDatabase database = new Database();
            IInputReader reader = new ConsoleReader();
            IOutputWriter writer = new ConsoleWriter();
            IHTMLParser htmlParser = new HTMLParser();

            ICrawler imagesCrawler = ImagesCrawler.GetInstance(database, htmlParser, writer);

            IEngine engine = new Engine(imagesCrawler, reader, writer);

            engine.Run();
        }
    }
}
