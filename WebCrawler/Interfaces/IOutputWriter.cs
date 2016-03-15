namespace WebCrawler.Interfaces
{
    public interface IOutputWriter
    {
        void Write(string line);

        void WriteLine(string line);
    }
}
