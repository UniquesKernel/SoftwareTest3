using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    internal class Turntable : ITurntable
    {
        private int speed;

        public Turntable()
        {
            setSpeed();
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }


        //speed from 1 to 100
        public void setSpeed(int speed = 50)
        {
            if (speed < 1)
            {
                this.speed = 1;
                return;
            }
            if(speed > 100)
            {
                this.speed = 100;
                return;
            }
            this.speed = speed;
        }

    }
}
