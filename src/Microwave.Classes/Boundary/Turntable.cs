using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Turntable : ITurntable
    {

        private IOutput myOutput;

        private bool isStarted;

        public Turntable(IOutput output)
        {
            myOutput = output;
            isStarted = false;

        }

        public void Start(int speed)
        {
            if(!isStarted)
            {
                if (speed < 1 || 100 < speed)
                {
                    throw new ArgumentOutOfRangeException("speed", speed, "Speed must be between 1 and 100 (Incl.)");
                }

                myOutput.OutputLine($"Turntable started with speed of: {speed}");
                isStarted = true;

            }
        }

        public void Stop()
        {
            if(isStarted)
            { 
                isStarted = false;
                myOutput.OutputLine($"Turntable stopped");
            }

        }


    }
}
