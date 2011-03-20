using NUnit.Framework;
using Rhino.Mocks;

namespace CodeBreakerMockingIo
{
    [TestFixture]
    public class GuessMarkerAcceptanceTest
    {
        private MockRepository mock;
        private IDataWriter dataWriter;
        private IDataReader dataReader;
        private IRandomSecret randomSecret;

        [SetUp]
        public void SetUp()
        {
            mock=new MockRepository();
            dataWriter = mock.DynamicMock<IDataWriter>();
            dataReader = mock.Stub<IDataReader>();
            randomSecret = mock.Stub<IRandomSecret>();
        }


        [TestCase("","xxxx","rgby")]
        [TestCase("m", "yxxx", "rgby")]
        [TestCase("m", "xyxx", "rgby")]
        [TestCase("m", "xxyx", "rgby")]
        [TestCase("m", "xxxb", "rgby")]
        [TestCase("mm", "yxxb", "rgby")]
        [TestCase("m", "yyxx", "rgby")]
        public void TestMarkingGuess(string result,string guess, string secret)
        {
            Expect.Call(dataReader.getInputData()).Return(guess);
            Expect.Call(randomSecret.NextSecret()).Return(secret);
            mock.ReplayAll();

            GuessMarker guessMarker = new GuessMarker(dataReader, dataWriter, randomSecret);
            guessMarker.Step();

            dataWriter.AssertWasCalled(x => x.WriteData(result));               
        }

        [TestCase("mm", "yxxb", "rgby")]
        [TestCase("m", "yyxx", "rgby")]
        public void TestModelMarkingGuesses(string result, string guess, string secret)
        {
            GuessMarker guessMarker = new GuessMarker(dataReader,dataWriter,secret);
            Assert.AreEqual(result,guessMarker.Mark(guess));
        }


        [Test]
        public void When_the_game_starts_should_say_welcome_to_master_mind()
        {
            GuessMarker guessMarker = new GuessMarker(dataReader,dataWriter,"rgby");

            mock.ReplayAll();
            
            guessMarker.start();

            dataWriter.AssertWasCalled(x => x.WriteData("Welcome to Master Mind. Please make a guess:"));
        }
     
    }
}