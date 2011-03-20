using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBreakerMockingIo
{
    public static class Extensions
    {
        public static string NTimes(this int aNumber,char c)
        {
            string toReturn = "";
            for (int i = 0;i<aNumber; i++)
            {
                toReturn += c;
            }
            return toReturn;
        }
    }
}
