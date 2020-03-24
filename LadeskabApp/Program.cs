using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
            ILogFile logFile = new LogFile(); 
            StationControl stationControl = new StationControl(reader, display, door, chargeControl, logFile);
            string id = "";

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R, T, A: ");
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
                        
                    case 'T':
                        if (door.DoorState == false)
                        {
                            Console.WriteLine("Bruger tilslutter telefon");
                            usbCharger.SimulateConnected(true);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Bruger kan ikke tilslutte telefon, da dør er lukket");
                            break;
                        }


                    case 'A':
                        if(door.DoorState == false)
                        {
                            Console.WriteLine("Bruger afkobler telefon");
                            usbCharger.SimulateConnected(false);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Bruger kan ikke afkoble telefon, da dør er lukket");
                            break;
                        }

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
