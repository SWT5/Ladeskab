using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Implementation;
using Ladeskab.Interfaces;
using Ladeskab.Simulator;
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
        //private DisplaySimulator displaySimulator;

        [SetUp]
        public void Setup()
        {
            _rfidReader = Substitute.For<IRFIDReader>();
            _display = Substitute.For<IDisplay>();
            _chargeControl = Substitute.For<IChargeControl>();
            _door = Substitute.For<IDoor>();
            _logFile = Substitute.For<ILogFile>();
            _uut = new StationControl(_rfidReader, _display, _door, _chargeControl, _logFile);
        }


        [Test]
        public void doorOpened_EventHandler_AvailableCase()
        {
            _door.DoorOpenEvent += Raise.Event();
            _display.Received(1).ConnectPhone(); //check if ConnectPhone is called and by that the eventHandler is called as well 
        }

        [Test]
        public void doorOpened_EventHandler_AvailableCase_disconnectPhone()
        {
            _chargeControl.IsConnected().Returns(true);
            _door.DoorOpenEvent += Raise.Event();
            _display.Received(1).DisconnectPhone(); //check if ConnectPhone is called and by that the eventHandler is called as well 
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
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<RfidDetectedEventArgs>>(this, new RfidDetectedEventArgs() { Id = "1" });
            _door.DoorOpenEvent += Raise.Event(); //open door event 
        }



        [Test]
        public void doorClosed_EventHandler_Called_DoorOpenedCase_ifStatement()
        {
            _chargeControl.IsConnected().Returns(true);
            _door.DoorOpenEvent += Raise.Event();//open door inorder to close it again
            _door.DoorCloseEvent += Raise.Event(); //_door.SimulateDoorCloses(); 
            _display.Received(1).LoadRfid(); //check if LoadRfid is called and by that the eventHandler is called as well 
        }



        [Test]
        public void doorClosed_EventHandler_Called_DoorOpenedCase_elseStatement()
        {
            _door.DoorOpenEvent += Raise.Event();//open door inorder to close it again
            _door.DoorCloseEvent += Raise.Event(); //_door.SimulateDoorCloses(); 
            _display.Received(1).NoPhoneConnected();
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
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<RfidDetectedEventArgs>>(this, new RfidDetectedEventArgs() { Id = "1" });
            _door.DoorCloseEvent += Raise.Event();
            _logFile.Received(1).LogError("The door is locked - can't close in this state");
        }


        [Test]
        public void RFIDReaderDetected_case_Available_isConnected()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<RfidDetectedEventArgs>>(this, new RfidDetectedEventArgs() { Id = "1" });
            _display.Received(1).PhoneStartCharging();
        }

        [Test]
        public void RFIDReaderDetected_case_ConnectionError()
        {
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<RfidDetectedEventArgs>>(this, new RfidDetectedEventArgs() { Id = "1" });
            _display.Received(1).ConnectionError();
        }

        [Test]
        public void correctID_disconnect_phone()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<RfidDetectedEventArgs>>(this, new RfidDetectedEventArgs() { Id = "1" });
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<RfidDetectedEventArgs>>(this, new RfidDetectedEventArgs() { Id = "1" });
            _display.Received(1).DisconnectPhone();
        }



        [Test]
        public void RFIDReaderDetected_Case_Locked_WrongRfid()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<RfidDetectedEventArgs>>(this, new RfidDetectedEventArgs() { Id = "1" });
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<RfidDetectedEventArgs>>(this, new RfidDetectedEventArgs() { Id = "2" });
            _display.Received(1).WrongRfid();
        }


        [Test]
        public void RFID_reader_DoorOpenState_case()
        {
            _door.DoorOpenEvent += Raise.Event(); //raise event to simulate door opens 
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<RfidDetectedEventArgs>>(this, new RfidDetectedEventArgs() { Id = "1" }); //raise event RFIDdetected
            _display.Received(1).WriteLine("The door is open");
        }
    }
}
