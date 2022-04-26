using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Turntable : ITurntable
    {
        private int speed;

        private IMotor motor;

        private bool isStarted;

        public Turntable(IMotor motor)
        {
            this.motor = motor;
            isStarted = false;

        }

        public void Start(in int speed)
        {
            if(!isStarted)
            {
                setSpeed(speed);
                this.motor.On();
                isStarted = true;
            }
        }

        public void Stop()
        {
            if(isStarted)
            { 
                this.motor.Off();
                isStarted = false;
            }

        }


        //speed from 1 to 100
        public void setSpeed(int speed)
        {
            if(this.speed == speed)
            {
                return;
            }

            if (speed < 1)
            {
                this.speed = 1;
            }
            else if (speed > 100)
            {
                this.speed = 100;
            }
            else
            {
                this.speed = speed;
            }
            this.motor.setSpeed(this.speed);
        }

    }
}
