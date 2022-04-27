using System;
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

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube,
            ITurntable turntable,
            IUserInterface ui) : this(timer, display, powerTube,turntable)
        {
            UI = ui;
        }

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube,
            ITurntable turntable)
        {
            myTimer = timer;
            myDisplay = display;
            myPowerTube = powerTube;
            myTurntable = turntable;

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

        public void OnTimerExpired(object sender, EventArgs e)
        {
            if (isCooking)
            {
                isCooking = false;
                myPowerTube.TurnOff();
                myTurntable.Stop();
                UI.CookingIsDone();
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
                throw new ArgumentOutOfRangeException("power", power, "Must be greater than 1(Incl.)");
            }

            int tmp = (int)Math.Ceiling(((double)power / 700) * 100);
            return  tmp > 100 ? 100 : tmp;
        }
    }
}