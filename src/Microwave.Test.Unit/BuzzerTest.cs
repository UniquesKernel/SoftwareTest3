using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System.Threading;


namespace Microwave.Test.Unit
{
    [TestFixture]
    public class BuzzerTest
    {
        private IOutput output;
        private Buzzer uut;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            uut = new Buzzer(output);
        }

        [Test]
        public void BuzzerStartsAndStops()
        {
            // We don't need an assert, as an exception would fail the test case
            uut.Buzz(1);
            output.Received(1).OutputLine("Buzzer On");
            output.Received(1).OutputLine("Buzzer Off");

        }

    }
}