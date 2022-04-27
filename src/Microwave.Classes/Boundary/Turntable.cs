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

        public void Start(int speed)
        {
            if(!isStarted)
            {
                if(SetSpeed(speed))
                {
                    this.motor.On();
                    isStarted = true;
                }

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
        public bool SetSpeed(int speed)
        {

            if (speed < 1 || 100 < speed || this.speed == speed)
            {
                return false;
            }
            this.speed = speed;
            this.motor.SetSpeed(this.speed);
            return true;
        }

    }
}
