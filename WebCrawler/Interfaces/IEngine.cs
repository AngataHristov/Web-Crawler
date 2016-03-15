namespace WebCrawler.Interfaces
{
    public interface IEngine
    {
        IInputReader Reader { get; }

        IOutputWriter Writer { get; }

        ICrawler ImageCrawler { get; }

        void Run();
    }
}
