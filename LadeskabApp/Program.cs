using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
using Ladeskab.Implementation;
using Ladeskab.Interfaces;
using Ladeskab.Simulator;
using UsbSimulator;

namespace LadeskabApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            IDoor door = new Door(); //temporary
            IRFIDReader reader = new RFIDReader();
            IUsbCharger usbCharger =new UsbChargerSimulator();
            IDisplay display = new DisplaySimulator();
            IChargeControl chargeControl =new ChargeControl(usbCharger);
            StationControl stationControl = new StationControl(reader, display, door, chargeControl);
            string id = "";

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.SimulateDoorOpens();
                        break;

                    case 'C':
                        door.SimulateDoorCloses();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();
                        id = idString;
                        reader.RegisterId(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
