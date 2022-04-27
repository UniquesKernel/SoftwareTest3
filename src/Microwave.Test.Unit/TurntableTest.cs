using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Unit
{   
    [TestFixture]
    public class TurntableTest
    {

        private Output output;
        private Turntable uut;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<Output>();
            uut = new Turntable(output);
        }


        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void Start_WasStopped_WithInrangeSpeed_CorrectOutput(int speed)
        {
            uut.Start(speed);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{speed}")));

        }


        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(101)]
        public void Start_WasStopped_WithOutofrangeSpeed_ThrowExeption(int speed)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.Start(speed));

        }

        [TestCase]
        public void Stop_WasStarted_CorrectOutput()
        {
            uut.Start(50);
            uut.Stop();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"stopped")));
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(101)]
        public void Start_WasStarted_CorrectOutcome(int speed)
        {
            uut.Start(50);
            output.ClearReceivedCalls();
            uut.Start(speed);
            output.DidNotReceive().OutputLine(Arg.Any<string>());

        }

        [TestCase]
        public void Stop_WasStopped_CorrectOutcome()
        {
            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

    }
}
