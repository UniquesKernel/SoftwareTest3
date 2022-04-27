using System;
using System.Collections.Generic;
using System.Text;

namespace Microwave.Classes.Interfaces
{
    public interface ITurntable
    {
        public void Start(int speed);
        public void Stop();
        public bool SetSpeed(int speed);
    }
}
