using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;

namespace Ladeskab.Simulator
{
    public class DisplaySimulator : IDisplay
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
            Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        public void WrongRfid()
        {
            Console.WriteLine("Forkert RFID tag");
        }

        public void DisconnectPhone()
        {
            Console.WriteLine("Tag din telefon ud af skabet og luk døren");
        }

        public void PhoneStartCharging()
        {
            Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        public void CloseDoorToReadRfid()
        {
            Console.WriteLine("Dør er åben! luk for at registrere RFID");
        }
    }
}
