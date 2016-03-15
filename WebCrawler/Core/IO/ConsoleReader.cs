namespace WebCrawler.Core.IO
{
    using System;

    using Interfaces;

    public class ConsoleReader : IInputReader
    {
        public string ReadNextLine()
        {
            string inputArgs = Console.ReadLine();

            return inputArgs;
        }
    }
}
