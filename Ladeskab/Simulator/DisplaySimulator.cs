using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Simulator
{
    public class DisplaySimulator
    {

        public void ConnectPhone()
        {
            Console.WriteLine("Tilslut telefon");
        }

        public void LoadRfid()
        {
            Console.WriteLine("Indlæs RFID");
        }

        public void ConnectionError()
        {
            Console.WriteLine("Tilslutningfejl");
        }

        public void WrongRfid()
        {
            Console.WriteLine("RFID fejl");
        }

        public void DisconnectPhone()
        {
            Console.WriteLine("Fjern telefon");
        }

        public void PhoneStartCharging()
        {
            Console.WriteLine("Telefonen oplader");
        }
    }
}
