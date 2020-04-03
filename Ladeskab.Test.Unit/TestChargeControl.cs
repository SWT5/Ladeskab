using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ladeskab.Implementation;
using Ladeskab.Interfaces;
using Ladeskab.Simulator;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using UsbSimulator;

namespace Ladeskab.Test.Unit
{
    class TestChargeControl
    {
        public int eventCount { get; set; }

        private ChargeControl uut_;
        private IUsbCharger _usbCharger;
        private IDisplay _display;
        

        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<IDisplay>();
            _usbCharger = Substitute.For<IUsbCharger>();
            uut_ = new ChargeControl(_usbCharger, _display);
        }

        /***    charging test     ***/
        [Test]
        public void startChargingIsCalled_inUsbCharger()
        {
            //Act
            uut_.StartCharge();
            //Assert
            _usbCharger.Received(1).StartCharge();      // received 1 call for startCharge
        }
        
        [Test]
        public void stopChargingIsCalled_inUsbCharger()
        {
            uut_.StopCharge();
            _usbCharger.Received(1).StopCharge();       // received 1 call for stopCharge
        }

        /***    connection test     ***/
        [Test]
        public void isConnectedTrue()
        {
            _usbCharger.Connected.Returns(true);
            Assert.That(uut_.IsConnected(), Is.EqualTo(true));
        }
        
        [Test]
        public void isConnectedFalse()
        {
            _usbCharger.Connected.Returns(false);
            Assert.That(uut_.IsConnected(), Is.EqualTo(false));
        }


        /***    CurrentState test     ***/
        [Test]
        public void CurrentValue_Fivehundred()
        {
            uut_.StartCharge();
            _usbCharger.CurrentValue.CompareTo(500);
            Assert
        }

        [Test]
        public void CurrentValue_two_and_a_half()
        {
            uut_.StopCharge();
            _usbCharger.CurrentValue.CompareTo(0.0);
        }

        /***    Event called Test   ***/

        [Test]
        public void eventCalledValue()
        {
            uut_.StartCharge();
            Assert.That(uut_.CurrentCharge, Is.EqualTo(500.0));
        }

        [Test]
        public void eventCalledTime()
        {
            _usbCharger.ReceivedWithAnyArgs(eventCount);
        }


        [Test]
        public void EventNotReceived()
        {
            _usbCharger.DidNotReceiveWithAnyArgs();
        }
    }
}
