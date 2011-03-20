using System;

namespace CodeBreakerMockingIo
{
    class ConsoleDataWriter: IDataWriter
    {
        public void WriteData(string data)
        {
            Console.Out.WriteLine(data);
        }
    }
}