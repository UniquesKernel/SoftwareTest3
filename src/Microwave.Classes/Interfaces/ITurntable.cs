﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Microwave.Classes.Interfaces
{
    internal interface ITurntable
    {
        public void Start(in int speed);
        public void Stop();
        public void setSpeed(int speed);
    }
}
