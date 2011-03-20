using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace CodeBreakerMockingIo
{
    [TestFixture]
    public class GuessMarkerTest
    {

        [TestCase("", "xxxx", "rgby")]
        [TestCase("m", "yxxx", "rgby")]
        [TestCase("m", "xyxx", "rgby")]
        [TestCase("m", "xxyx", "rgby")]
        [TestCase("m", "xxxb", "rgby")]
        [TestCase("mm", "yxxb", "rgby")]
        [TestCase("m", "yyxx", "rgby")]
        [TestCase("p","rxxx","rgby")]
        [TestCase("pp","rgxx","rgby")]
        [TestCase("ppp","rgbx","rgby")]
        public void TestModelMarkingGuesses(string result, string guess, string secret)
        {
            GuessMarker guessMarker = new GuessMarker(secret);
            Assert.AreEqual(result, guessMarker.Mark(guess));
        }

    }
}
