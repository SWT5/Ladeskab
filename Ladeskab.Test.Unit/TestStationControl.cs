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

namespace Ladeskab.Test.Unit
{
    class TestStationControl
    {
        private StationControl _uut;
        private IRFIDReader _rfidReader;
        private IDisplay _display;
        private IChargeControl _chargeControl;
        private IDoor _door;
        public int EventCount { get; set; }


        [SetUp]
        public void Setup()
        {
            _rfidReader = Substitute.For<IRFIDReader>();
            _display = Substitute.For<IDisplay>();
            _chargeControl= Substitute.For<IChargeControl>();
            _door = new Door(); //Substitute.For<IDoor>();
            _uut = new StationControl(_rfidReader, _display, _door, _chargeControl);
        }


        [Test]
        public void RFID_reader_Lockedstate_ifStatementCheck()
        {
            _door.SimulateDoorOpens();
            _rfidReader.RegisterId("1"); //raise event 

        }


        [Test]
        public void doorOpened_EventHandler_Called()
        {
            _door.SimulateDoorOpens();
            _display.Received(1).ConnectPhone(); //check if ConnectPhone is called and by that the eventHandler is called as well 
        }


        [Test]
        public void doorClosed_EventHandler_Called()
        {
            _door.SimulateDoorOpens(); //open door inorder to close it again
            _door.SimulateDoorCloses(); 
            _display.Received(1).LoadRfid(); //check if LoadRfid is called and by that the eventHandler is called as well 
        }


        [Test]
        public void doorOpened_EventHandler_NotCalled()
        {
            _door.SimulateDoorOpens(); //It's able to open the first time
            _door.SimulateDoorOpens(); //Cannot open again hence door already open 
            _display.Received(1).ConnectPhone(); //Expected only one call
        }


        [Test]
        public void doorClosed_EventHandler_NotCalled()
        {
            _door.SimulateDoorCloses();
            _display.Received(0).LoadRfid(); //expect 0 calls to LoadRfid
        }

        [Test]
        public void RFIDReaderDetected_case_Available_isConnected()
        {
            
            _chargeControl.IsConnected();
            _rfidReader.RegisterId("1");    

        }
    }
}
