﻿using System;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class CookControllerTest
    {
        private CookController uut;

        private IUserInterface ui;
        private ITimer timer;
        private IDisplay display;
        private IPowerTube powerTube;
        private ITurntable turntable;

        [SetUp]
        public void Setup()
        {
            ui = Substitute.For<IUserInterface>();
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();
            powerTube = Substitute.For<IPowerTube>();
            turntable = Substitute.For<ITurntable>();

            uut = new CookController(timer, display, powerTube, turntable, ui);
        }

        [Test]
        public void StartCooking_ValidParameters_TimerStarted()
        {
            uut.StartCooking(50, 60);

            timer.Received().Start(60);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            uut.StartCooking(50, 60);

            powerTube.Received().TurnOn(50);
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(700)]
        [TestCase(701)]
        [TestCase(9001)]
        public void StartCooking_ValidParameters_TurntableStart(int power)
        {
            uut.StartCooking(power, 60);
            int tmp = (int)Math.Ceiling(((double)power / 700) * 100);

            turntable.Received().Start(tmp > 100 ? 100 : tmp);
        }



        [TestCase(-1)]
        [TestCase(0)]
        public void StartCooking_InvalidParameters_ThrowExeption(int power)
        { 
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.StartCooking(power, 60));

        }


        [Test]
        public void Cooking_TimerTick_DisplayCalled()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(115);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(1, 55);
        }

        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            powerTube.Received().TurnOff();
        }

        [Test]
        public void Cooking_TimerExpired_TurntableStop()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            turntable.Received().Stop();
        }


        [Test]
        public void Cooking_TimerExpired_UICalled()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            ui.Received().CookingIsDone();
        }

        [Test]
        public void Cooking_Stop_PowerTubeOff()
        {
            uut.StartCooking(50, 60);
            uut.Stop();

            powerTube.Received().TurnOff();
        }

        [Test]
        public void Cooking_Stop_TurntableStop()
        {
            uut.StartCooking(50, 60);
            uut.Stop();

            turntable.Received().Stop();
        }

    }
}