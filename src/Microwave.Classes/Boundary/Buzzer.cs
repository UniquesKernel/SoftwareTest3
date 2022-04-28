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
        private bool _isOn = false;
        public Buzzer(IOutput output)
        {
            _output = output;
        }

        public bool IsOn
        {
            get { return _isOn; }
        }

        public void BuzzerOn()
        {
            if (!_isOn)
            {
            _isOn = true;
            _output.OutputLine("Buzzer On");
            }
        }

        public void BuzzerOff()
        {
            if (_isOn)
            {
                _isOn = false;
                _output.OutputLine("Buzzer Off");
            }
        }
    }
}
