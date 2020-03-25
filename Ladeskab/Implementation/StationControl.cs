﻿using System;
using System.CodeDom;
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
        private ILogFile _logFile;
        //private IRFIDReader _rfidReader; 
        private string _oldId;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IRFIDReader rfidreader, IDisplay display, IDoor door, IChargeControl chargeControl, ILogFile logFile)
        {
            //Objects being instantiated 
            _display = display;
            _door = door;
            _chargeControl = chargeControl;
            _logFile = logFile;
            /*_rfidReader = rfidreader;*/

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
                    if (_chargeControl.IsConnected())
                    {
                        _door.LockDoor();
                        _chargeControl.StartCharge();
                        _oldId = e.Id; //used to be id instead of e
                        _logFile.LogDoorLocked(_oldId);

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
                        _logFile.LogDoorUnlocked(e.Id);

                        _display.DisconnectPhone();
                        //Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.WrongRfid();
                        _logFile.LogError("Forkert RFID tag med: " + e.Id);
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        public void DoorOpened(object sender, EventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Locked:
                    _logFile.LogError("Can't open when locked");
                    break;
                case LadeskabState.DoorOpen: //Not expected case 
                    _logFile.LogError("Not expected to open when already opened");
                    break;
                case LadeskabState.Available:
                    if (_chargeControl.IsConnected() == false)
                    {
                        _display.ConnectPhone();
                        _state = LadeskabState.DoorOpen;
                        break;
                    }
                    else
                    {
                        _display.DisconnectPhone();
                        _state = LadeskabState.DoorOpen;
                        break;
                    }
            }
        }

        public void DoorClosed(object sender, EventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.DoorOpen:
                    //_display.LoadRfid();
                    //_state = LadeskabState.Available;
                    //break;
                    if (_chargeControl.IsConnected())
                    {
                        _display.LoadRfid();
                        _state = LadeskabState.Available;
                        break;
                    }
                    else
                    {
                        _display.NoPhoneConnected();
                        _state = LadeskabState.Available;
                        break;
                    }

                case LadeskabState.Locked:
                    _logFile.LogError("The door is locked - can't close in this state");
                    break;
                case LadeskabState.Available:
                    _logFile.LogError("Door is already closed");
                    break;
            }
        }




    }
}
