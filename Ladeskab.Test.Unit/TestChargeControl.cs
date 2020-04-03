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
        //[Test]
        //public void CurrentValue_Fivehundred()
        //{
        //    uut_.StartCharge();
        //    _usbCharger.CurrentValue.CompareTo(500);

        //}

        //[Test]
        //public void CurrentValue_two_and_a_half()
        //{
        //    uut_.StopCharge();
        //    _usbCharger.CurrentValue.CompareTo(0.0);
        //}

        /***    Event called Test   ***/

        [TestCase(0)]
        [TestCase(4.8)]
        [TestCase(5)]
        [TestCase(5.2)]
        [TestCase(499)]
        [TestCase(500)]
        [TestCase(501)]
        public void ChargeControl_Receivces_currentValue_from_CurrentValueEvent(double newCurrent)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() {Current = newCurrent});
            Assert.That(uut_.CurrentCharge, Is.EqualTo(newCurrent));
        }

        // test eventhandler HandleCurrentChangedEvent interaktion med IDisplay og IUSBCharger
        [TestCase(0)]
        public void HandleCurrentChangedEvent_currentValue_Equal_zero(double newCurrent)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() {Current = newCurrent});
            _display.Received(1).WriteLine("Ingen Telefonen tilsuttet - oplader ikke...  aktuel ladestroem: " + newCurrent);
        }

        [TestCase(0.3)]
        [TestCase(4.6)]
        [TestCase(5)]
        public void HandleCurrentChangedEvent_currentValue_between_0_and_5(double newCurrent)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() {Current = newCurrent});
            _display.Received(1).WriteLine("Telefonen er fuldt opladt...  aktuel ladestroem: " + newCurrent);
        }

        [TestCase(5.2)]
        [TestCase(500)]
        [TestCase(499.5)]
        public void HandleCurrentChangedEvent_currentValue_between_5_and_500(double newCurrent)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() {Current = newCurrent});
            _display.Received(1).WriteLine("Telefonen oplader...  aktuel ladestroem: " + newCurrent);
        }

        [TestCase(500.5)]
        [TestCase(550)]
        public void HandleCurrentChangedEvent_currentValue_over_500(double newCurrent)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() {Current = newCurrent});
            _display.Received(1).WriteLine("FEJL!!!! Der er noget galt med ladestrømmen - frakobl telefonen...  aktuel ladestroem: " + newCurrent);
        }

    }
}
