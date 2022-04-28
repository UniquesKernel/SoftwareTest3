using System;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Button startCancelButton = new Button();
            Button increaseTimeButton = new Button();
            Button decreaseTimeButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output);

            Light light = new Light(output);

            Buzzer buzzer = new Buzzer(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube, buzzer);

            UserInterface ui = new UserInterface(
                powerButton, 
                timeButton, increaseTimeButton, decreaseTimeButton,
                startCancelButton, door, display, light, cooker);

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            
            powerButton.Press();

            timeButton.Press();

            startCancelButton.Press();

            System.Console.WriteLine("When you press enter, the program will stop");
            
            Thread.Sleep(5000);

            increaseTimeButton.Press();

            Thread.Sleep(5000);

            decreaseTimeButton.Press();

            // The simple sequence should now run

            // Wait for input

            System.Console.ReadLine();
        }
    }
}
