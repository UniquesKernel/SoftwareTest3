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
            IUserInterface ui) : this(timer, display, powerTube,turntable, buzzer)
        {
            UI = ui;
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
            myTurntable.Start(SpeedPowerConverter(power));
            myPowerTube.TurnOn(power);
            myTimer.Start(time);
            isCooking = true;
        }

        public void Stop()
        {
            isCooking = false;
            myPowerTube.TurnOff();
            myTurntable.Stop();
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
                myTurntable.Stop();
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
            if(power < 1)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between greater than 1(Incl.)");
            }
            int tmp = (int)((power / 700) * 100);
            return  tmp > 100 ? 100 : tmp;
        }
    }
}
