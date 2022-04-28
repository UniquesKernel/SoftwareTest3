using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;

        private readonly int? _maxPower;


        public PowerTube(IOutput output, in int maxPower = 700)
        {
            if(maxPower < 1)
            {
                throw new ArgumentOutOfRangeException("maxPower", maxPower, "PowerTube Maximum Power must be greater than 0");
            }
            myOutput = output;
            _maxPower = maxPower;
            myOutput.OutputLine($"PowerTube Maximum Power is set to {_maxPower}");
        }

        public void TurnOn(int power)
        {
            if (power < 1 || _maxPower < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and" + _maxPower + " (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}