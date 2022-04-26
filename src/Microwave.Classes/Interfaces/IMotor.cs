using System;
using System.Collections.Generic;
using System.Text;

namespace Microwave.Classes.Interfaces
{
    internal interface IMotor
    {
        public void On();

        public void Off();

        public void setSpeed(int speed);
    }
}
