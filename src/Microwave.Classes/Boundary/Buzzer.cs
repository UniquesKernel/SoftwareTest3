using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Interfaces;
using System.Threading;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {
        private IOutput _output = null;
        public Buzzer(IOutput output)
        {
            _output = output;
        }
        public void Buzz(int milliseconds)
        {
            _output.OutputLine("Buzzer On");
            Thread.Sleep(milliseconds);
            _output.OutputLine("Buzzer Off");
        }
    }
}
