using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBreakerMockingIo
{
    public interface IRandomSecret
    {
        string NextSecret();
    }

    class RandomSecret : IRandomSecret
    {
        private char[] colors = {'r', 'g', 'b', 'y', 'c'};
        Random random = new Random((int) Convert.ToInt64(DateTime.Now.Ticks));
        public string NextSecret()
        {
            string secret = "";
            for (int i = 0; i < 4;i++ )
            {
                secret += colors[random.Next()%5];
            }
            return secret;
        }
    }
}
