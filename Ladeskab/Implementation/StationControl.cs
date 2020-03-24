using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Implementation;
using UsbSimulator;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IUsbCharger _charger;
        private IDisplay _display;
        private IDoor _door;
        private IChargeControl _chargeControl;
        private IRFIDReader _rfidReader;
        private string _oldId;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IRFIDReader rfidreader, IDisplay display, IDoor door, IChargeControl chargeControl)
        {
            //Objects being instantiated 
            _display = display;
            _door = door;
            _chargeControl = chargeControl;
            _rfidReader = rfidreader;

            //event connections 
            rfidreader.RfidDetectedEvent += RfidDetected; //This is the subscription to RFIDevents 
            door.DoorOpenEvent += DoorOpened; //Subscription to doorOpenEvent 
            door.DoorCloseEvent += DoorClosed; //subscription to doorCloseEvent 
            _state = LadeskabState.Available;
        }


        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(object sender, RfidDetectedEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _chargeControl.StartCharge();
                        _oldId = e.Id; //used to be id instead of e
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", e.Id); //used to be id instead of e
                        }

                        _display.PhoneStartCharging();
                        //Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.ConnectionError();
                        //Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (e.Id == _oldId) //used to be id instead of e
                    {
                        _chargeControl.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", e.Id); //used to be id instead of e
                        }

                        _display.DisconnectPhone();
                        //Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.WrongRfid();
                        //Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        public void DoorOpened(object sender, EventArgs e)
        {
            _display.ConnectPhone();
        }

        public void DoorClosed(object sender, EventArgs e)
        {
            _display.LoadRfid(); //
          
        }




    }
}
