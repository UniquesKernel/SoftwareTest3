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

        private IMotor motor;
        private Turntable uut;

        [SetUp]
        public void Setup()
        {
            motor = Substitute.For<IMotor>();
            uut = new Turntable(motor);
        }


        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void Start_WithInrangeSpeed_InrangeSetMotorSpeed(int speed)
        {
            uut.Start(speed);
            motor.Received().On();
            motor.Received().SetSpeed(Arg.Is<int>(sp => sp == speed));
        }


        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(101)]
        public void Start_WithOutofrangeSpeed_BoundarySetMotorSpeed(int speed)
        {
            uut.Start(speed);
            motor.DidNotReceive().On();
            motor.DidNotReceive().SetSpeed(Arg.Any<int>());
        }

        [Test]
        public void SetSpeed_WasTheSameSpeed_NoSetMotorSpeed()
        {
            uut.Start(10);
            motor.ClearReceivedCalls();
            uut.SetSpeed(10);
            motor.DidNotReceive().SetSpeed(Arg.Any<int>());
        }

    }
}
