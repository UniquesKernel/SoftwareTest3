using System;
using System.Threading;
using NUnit.Framework;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class TimerTest
    {
        private Timer uut;

        [SetUp]
        public void Setup()
        {
            uut = new Timer();
        }

        [Test]
        public void Start_TimerTick_ShortEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimerTick += (sender, args) => pause.Set();
            uut.Start(2);

            // wait for a tick, but no longer
            Assert.That(pause.WaitOne(1100));
        }

        [Test]
        public void Start_TimerTick_LongEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimerTick += (sender, args) => pause.Set();
            uut.Start(2);

            // wait shorter than a tick, shouldn't come
            Assert.That(!pause.WaitOne(900));
        }

        [Test]
        public void Start_TimerExpires_ShortEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();
            uut.Start(2);

            // wait for expiration, but not much longer, should come
            Assert.That(pause.WaitOne(2100));
        }

        [Test]
        public void Start_TimerExpires_LongEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();
            uut.Start(2);

            // wait shorter than expiration, shouldn't come
            Assert.That(!pause.WaitOne(1900));
        }

        [Test]
        public void Start_TimerTick_CorrectNumber()
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            int notifications = 0;

            uut.Expired += (sender, args) => pause.Set();
            uut.TimerTick += (sender, args) => notifications++;

            uut.Start(2);

            // wait longer than expiration
            Assert.That(pause.WaitOne(2100));

            Assert.That(notifications, Is.EqualTo(2));
        }

        [Test]
        public void Stop_NotStarted_NoThrow()
        {
            Assert.That( () => uut.Stop(), Throws.Nothing);
        }

        [Test]
        public void Stop_Started_NoTickTriggered()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimerTick += (sender, args) => pause.Set();

            uut.Start(2000);
            uut.Stop();

            Assert.That(!pause.WaitOne(1100));
        }

        [Test]
        public void Stop_Started_NoExpiredTriggered()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();

            uut.Start(2000);
            uut.Stop();

            Assert.That(!pause.WaitOne(2100));
        }

        [Test]
        public void Stop_StartedOneTick_NoExpiredTriggered()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();
            uut.TimerTick += (sender, args) => uut.Stop();

            uut.Start(2000);

            Assert.That(!pause.WaitOne(2100));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Tick_Started_TimeRemainingCorrect(int ticks)
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            int ticksGone = 0;
            uut.TimerTick += (sender, args) =>
            {
                ticksGone++;
                if (ticksGone >= ticks)
                    pause.Set();
            };
            uut.Start(5);

            // wait for ticks, only a little longer
            pause.WaitOne(ticks * 1000 + 100);

            Assert.That(uut.TimeRemaining, Is.EqualTo(5-ticks*1));
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void AdjustTime_Change_TimeRemainingCorrect(int timeAdjustment)
        {
            uut.Start(10);
            uut.Stop();

            Assert.That(uut.TimeRemaining, Is.EqualTo(10));

            int expectedTimeRemaining = uut.TimeRemaining + timeAdjustment;
            uut.AdjustTime(timeAdjustment);

            Assert.That(uut.TimeRemaining, Is.EqualTo(expectedTimeRemaining));
        }

        [TestCase(1,1)]
        [TestCase(1,0)]
        [TestCase(1,-1)]
        [TestCase(2,1)]
        [TestCase(2,0)]
        [TestCase(2,-1)]
        [TestCase(3,1)]
        [TestCase(3,0)]
        [TestCase(3,-1)]
        [TestCase(4,1)]
        [TestCase(4,0)]
        [TestCase(4,-1)]

        public void AdjustTime_Tick_Started_TimeRemainingCorrect(int ticks, int timeAdjustment)
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            int ticksGone = 0;
            uut.TimerTick += (sender, args) =>
            {
                ticksGone++;
                if (ticksGone >= ticks)
                    pause.Set();
            };
            int timeStart = 4;
            int expectedTimeRemaining = (timeStart + timeAdjustment) - ticks < 0 ? 0 : (timeStart + timeAdjustment) - ticks*1 ;
            uut.Start(timeStart);

            // wait for ticks, only a little longer
            pause.WaitOne(ticks * 1000 + 100);

            // Add or take away extra time
            uut.AdjustTime(timeAdjustment);
            
            Assert.That(uut.TimeRemaining, Is.EqualTo(expectedTimeRemaining));
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-1)]
        public void AdjustTime_TimerTick_CorrectNumber(int timeAdjustment)
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            int notifications = 0;

            uut.Expired += (sender, args) => pause.Set();
            uut.TimerTick += (sender, args) => notifications++;

            int startTime = 5;
            uut.Start(startTime);

            pause.WaitOne(2*1000+100);

            uut.AdjustTime(timeAdjustment);

            Assert.That(pause.WaitOne((startTime+timeAdjustment - notifications)*1000+100));
            Assert.That(notifications, Is.EqualTo(startTime+timeAdjustment));

        }
        
        
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-1)]
        public void AdjustTime_TimeExpires_ShortEnough(int timeAdjustment)
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();
            
            int startTime = 5;
            int waitTime = 1;
            uut.Start(startTime);

            pause.WaitOne(waitTime * 1000 + 100);

            uut.AdjustTime(timeAdjustment);

            // wait for expiration, but not much longer, should come
            Assert.That(pause.WaitOne((startTime + timeAdjustment - waitTime)*1000+100));
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-1)]
        public void AdjustTime_TimeExpires_LongEnough(int timeAdjustment)
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();

            int startTime = 5;
            int waitTime = 1;
            uut.Start(startTime);

            pause.WaitOne(waitTime * 1000 + 100);

            uut.AdjustTime(timeAdjustment);

            // wait shorter than expiration, shouldn't come
            Assert.That(!pause.WaitOne((startTime + timeAdjustment - waitTime) * 1000 - 200));
        }

    }
}