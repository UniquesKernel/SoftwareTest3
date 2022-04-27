using System;
using System.Threading;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Controllers
{
    public class CookController : ICookController
    {
        // Since there is a 2-way association, this cannot be set until the UI object has been created
        // It also demonstrates property dependency injection
        public IUserInterface UI { set; private get; }

        private bool isCooking = false;

        private IDisplay myDisplay;
        private IPowerTube myPowerTube;
        private ITimer myTimer;
        private ITurntable myTurntable;
        private IBuzzer myBuzzer;

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube,
            ITurntable turntable,
            IBuzzer buzzer,
            IUserInterface ui) : this(timer, display, powerTube, buzzer)
        {
            UI = ui;
            myTurntable = turntable;
        }

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube,
            IPowerTube powerTube, 
            IBuzzer buzzer,
            ITurntable turntable)
        {
            myTimer = timer;
            myDisplay = display;
            myPowerTube = powerTube;
            myTurntable = turntable;
            myBuzzer = buzzer;

            timer.Expired += new EventHandler(OnTimerExpired);
            timer.TimerTick += new EventHandler(OnTimerTick);
        }

        public void StartCooking(int power, int time)
        {
            myPowerTube.TurnOn(power);
            myTimer.Start(time);
            isCooking = true;
        }

        public void Stop()
        {
            isCooking = false;
            myPowerTube.TurnOff();
            myTimer.Stop();
        }

        public void AdjustCookingTime(int timeAdjustment)
        {
            myTimer.AdjustTime(timeAdjustment);
        }

        public void OnTimerExpired(object sender, EventArgs e)
        {
            if (isCooking)
            {
                isCooking = false;
                myPowerTube.TurnOff();
                UI.CookingIsDone();
                for(int i = 0; i < 3; i++)
                {
                    myBuzzer.BuzzerOn();
                    Thread.Sleep(1000);
                    myBuzzer.BuzzerOff();
                    Thread.Sleep(1000);
                }
            }
        }

        public void OnTimerTick(object sender, EventArgs e)
        {
            if (isCooking)
            {
                int remaining = myTimer.TimeRemaining;
                myDisplay.ShowTime(remaining / 60, remaining % 60);
            }
        }

        private int SpeedPowerConverter(int power)
        {
            double tmp = power * 0.1;
            return 
        }
    }
}
