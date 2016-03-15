namespace WebCrawler.Core.IO
{
    using System;
    using System.Text;
    using Interfaces;

    public class ConsoleWriter : IOutputWriter
    {
        private readonly StringBuilder outputBuffer;

        public ConsoleWriter()
        {
            this.outputBuffer = new StringBuilder();
            this.AutoFlush = true;
        }

        public bool AutoFlush { get; private set; }


        public void Write(string line)
        {
            this.outputBuffer.Append(line);

            if (this.AutoFlush)
            {
                this.Flush();
            }
        }

        public void WriteLine(string line)
        {
            this.outputBuffer.AppendLine(line);

            if (this.AutoFlush)
            {
                this.Flush();
            }
        }

        private void Flush()
        {
            Console.Write(this.outputBuffer);

            this.outputBuffer.Clear();
        }
    }
}
