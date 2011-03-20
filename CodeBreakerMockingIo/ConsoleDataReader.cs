using System;

namespace CodeBreakerMockingIo
{
    class ConsoleDataReader: IDataReader
    {
        public string getInputData()
        {
            return Console.ReadLine();
        }
    }
}