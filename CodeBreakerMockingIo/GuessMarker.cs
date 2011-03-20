using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodeBreakerMockingIo
{
    class GuessMarker
    {
        private IDataReader dataRead;
        private IDataWriter dataWrite;
        private string secret;


        public GuessMarker(string secret)
        {
            this.secret = secret;
        }


        public GuessMarker(IDataReader dataRead, IDataWriter dataWrite,string secret)
        {
            this.secret = secret;
            this.dataRead = dataRead;
            this.dataWrite = dataWrite;
        }

        public GuessMarker(IDataReader dataRead, IDataWriter dataWrite, IRandomSecret randomSecret)
        {
            this.secret = randomSecret.NextSecret();
            this.dataRead = dataRead;
            this.dataWrite = dataWrite;
        }


        public static void Main(string[] args)
        {
            IDataReader dataRead = new ConsoleDataReader();
            IDataWriter dataWrite = new ConsoleDataWriter();
            string secret;
            GuessMarker marker;

            if (args != null && args.Length > 0)
            {
                secret = args[0];
                marker = new GuessMarker(dataRead, dataWrite, secret);
            }
            else
            {
                marker = new GuessMarker(dataRead, dataWrite, new RandomSecret());
            }

            marker.start();
            while (true)
            {
                marker.Step();
            }
        }


        public void Step()
        {
            dataWrite.WriteData(Mark(dataRead.getInputData()));
        }


        public string Mark(string guess)
        {
            string positionals = PositionalMatches(guess);
            string nonPositionals = NonPositionalMatches(guess);
            string mark = positionals + nonPositionals;
            mark = mark.Substring(0, mark.Length - positionals.Length);
            return mark;
        }

        private string NonPositionalMatches(string guess)
        {
            List<char> lSecret = secret.ToList();
            string matches = "";
            foreach (char c in guess)
            {
                if (lSecret.Contains(c))
                {
                    matches += "m";
                    lSecret.Remove(c);
                }
            }
            return matches;
        }


        private string PositionalMatches(string guess)
        {
            int index = 0;
            return guess.Aggregate("", (current, next) => current += secret[index++] == next ? "p" : "");
        }


        internal void start()
        {
            dataWrite.WriteData("Welcome to Master Mind. Please make a guess:");
        }
    }
}
