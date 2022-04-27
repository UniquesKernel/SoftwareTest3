using System;
using System.Collections.Generic;
using System.Text;

namespace Microwave.Classes.Interfaces
{
    public interface IMotor
    {
        public void On();

        public void Off();

        public void SetSpeed(int speed);
    }
}
