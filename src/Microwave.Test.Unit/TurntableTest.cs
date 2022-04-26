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
        public void Start_WasCreated_CorrectMotorCall(int speed)
        {
            uut.Start(speed);
            motor.Received().On();
            motor.Received().setSpeed(Arg.Is<int>(sp => sp == speed));
        }


        [TestCase(-10)]
        [TestCase(0)]
        [TestCase(101)]
        public void Start_WasCreated_IncorrectMotorCall(int speed)
        {
            uut.Start(speed);
            motor.Received().On();
            motor.Received().setSpeed(Arg.Is<int>(sp => sp == (speed < 1 ? 1 : 100 < speed ? 100 : speed)));
        }

        [Test]
        public void SetSpeed_WasTheSameSpeed()
        {
            uut.Start(10);
            uut.setSpeed(10);
            motor.DidNotReceive().setSpeed(Arg.Is<int>(sp => sp == 10));
        }

    }
}
