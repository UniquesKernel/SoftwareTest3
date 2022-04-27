using System;
using System.Collections.Generic;
using System.Text;

namespace Microwave.Classes.Interfaces
{
    public interface IBuzzer
    {
        public bool IsOn
        {
            get;
        }
        void BuzzerOn();
        void BuzzerOff();
    }
}
