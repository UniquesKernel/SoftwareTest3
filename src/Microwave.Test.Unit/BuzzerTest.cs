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
        public void BuzzerStartsOff()
        {
            Assert.That(uut.IsOn, Is.False);
        }

        [Test]
        public void BuzzerOn()
        {
            uut.BuzzerOn();
            output.Received(1).OutputLine("Buzzer On");
            Assert.That(uut.IsOn, Is.True);
        }
        
        [Test]
        public void BuzzerOnThenOff()
        {
            uut.BuzzerOn();
            output.Received(1).OutputLine("Buzzer On");
            Assert.That(uut.IsOn, Is.True);
            uut.BuzzerOff();
            output.Received(1).OutputLine("Buzzer Off");
            Assert.That(uut.IsOn, Is.False);

        }

        [Test]
        public void BuzzerOnWhenOn()
        {
            uut.BuzzerOn();
            uut.BuzzerOn();
            output.Received(1).OutputLine("Buzzer On");
            Assert.That(uut.IsOn, Is.True);

        }

        [Test]
        public void BuzzerOffWhenOff()
        {
            uut.BuzzerOff();
            output.DidNotReceive().OutputLine("Buzzer Off");
            Assert.That(uut.IsOn, Is.False);
        }


    }
}