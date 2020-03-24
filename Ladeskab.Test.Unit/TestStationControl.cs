using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Implementation;
using Ladeskab.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using UsbSimulator;

namespace Ladeskab.Test.Unit
{
    class TestStationControl
    {
        private StationControl _uut;
        private IRFIDReader _rfidReader;
        private IDisplay _display;
        private IChargeControl _chargeControl;
        private ILogFile _logFile;
        private IDoor _door;

        [SetUp]
        public void Setup()
        {
            _rfidReader = Substitute.For<IRFIDReader>();
            _display = Substitute.For<IDisplay>();
            _chargeControl= Substitute.For<IChargeControl>();
            _door = Substitute.For<IDoor>();
            _logFile = Substitute.For<ILogFile>();
            _uut = new StationControl(_rfidReader, _display, _door, _chargeControl, _logFile);
        }

        [Test]
        public void RFID_reader_DoorOpenState_case()
        {
            _door.DoorOpenEvent += Raise.Event(); //raise event to simulate door opens 
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler <RfidDetectedEventArgs>>(this,new RfidDetectedEventArgs() {Id = "1"});
            //_rfidReader.RegisterId("1"); //raise event 

        }


        [Test]
        public void doorOpened_EventHandler_AvailableCase()
        {
            
            _door.DoorOpenEvent += Raise.Event();
            //_door.SimulateDoorOpens();
            _display.Received(1).ConnectPhone(); //check if ConnectPhone is called and by that the eventHandler is called as well 
        }

        [Test]
        public void doorOpened_EventHandler_DoorOpenedCase()
        {
            _door.DoorOpenEvent += Raise.Event(); //first time open door
            _door.DoorOpenEvent += Raise.Event(); //not able to open again
            _logFile.Received(1).LogError("Not expected to open when already opened");
        }

        [Test]
        public void doorOpened_EventHandler_LockedCase()
        {
            _uut.setDoorState_Locked();
            _door.DoorOpenEvent += Raise.Event(); //open door event 
        }



        [Test]
        public void doorClosed_EventHandler_Called_DoorOpenedCase()
        {
            _door.DoorOpenEvent += Raise.Event();
            //_door.SimulateDoorOpens(); //open door inorder to close it again
            _door.DoorCloseEvent += Raise.Event();
            //_door.SimulateDoorCloses(); 
            _display.Received(1).LoadRfid(); //check if LoadRfid is called and by that the eventHandler is called as well 
        }

        [Test]
        public void doorClosed_EventHandler_AvailableCase()
        {
            _door.DoorCloseEvent += Raise.Event();
            _logFile.Received(1).LogError("Door is already closed");
        }

        [Test]
        public void doorClosed_EventHandler_LockedCase()
        {
            _uut.setDoorState_Locked();
            _door.DoorCloseEvent += Raise.Event();
            _logFile.Received(1).LogError("The door is locked - can't close in this state");

        }


        [Test]
        public void RFIDReaderDetected_case_Available_isConnected()
        {
            _rfidReader.RfidDetectedEvent +=
                Raise.Event<EventHandler<RfidDetectedEventArgs>>(this, new RfidDetectedEventArgs() {Id = "1"});
            _display.Received(1).PhoneStartCharging();
        }
    }
}
