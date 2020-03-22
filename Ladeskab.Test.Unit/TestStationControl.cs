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
        private int eventCount { get; set; }


        [SetUp]
        public void Setup()
        {
            _rfidReader = Substitute.For<IRFIDReader>();
            _display = Substitute.For<IDisplay>();
            _chargeControl= Substitute.For<IChargeControl>();
            _door = Substitute.For<IDoor>();
            _uut = new StationControl(_rfidReader, _display, _door, _chargeControl);
        }





        [Test]
        public void doorOpened_IsTrue()
        {
            //_door.DoorOpenEvent += (o,e) => _{};  //subscribe 
            _door.SimulateDoorOpens();
            _door.DoorOpenEvent += Raise.Event();
            Assert.That(_uut.DoorOpened(_door,EventArgs.Empty), e);
            //_uut.Received(1).DoorOpened(_door,EventArgs.Empty);
        }


        [Test]
        public void doorClosed_Istrue()
        {

        }


    }
}
