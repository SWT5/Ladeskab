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
        public int eventCount { get; set; }
        private IUsbCharger usbCharger_;        //Substitute: Fake
        private IDisplay display_;
        private DisplaySimulator displaySimulator;
        private CurrentEventArgs eventArgs = new CurrentEventArgs();



        [SetUp]
        public void Setup()
        {
            usbCharger_ = Substitute.For<IUsbCharger>();
            display_ = Substitute.For<IDisplay>();
            uut_ = new ChargeControl(usbCharger_);
            displaySimulator = Substitute.For<DisplaySimulator>();
        }

        /***    charging test     ***/
        [Test]
        public void startCharging()
        {
            //Act
            uut_.StartCharge();
            //Assert
            usbCharger_.Received(1).StartCharge();      // received 1 call for startCharge
        }
        
        [Test]
        public void stopCharging()
        {
            uut_.StopCharge();
            usbCharger_.Received(1).StopCharge();       // received 1 call for stopCharge
        }

        /***    connection test     ***/
        [Test]
        public void isConnectedTrue()
        {
            uut_.StartCharge();
            usbCharger_.Connected.Equals(true);
        }
        
        [Test]
        public void isConnectedFalse()
        {
            uut_.StopCharge();
            usbCharger_.Connected.Equals(false);
        }

        [Test]
        public void IsConnectedBool()
        {
            uut_.IsConnected();
            usbCharger_.Connected.Equals(true);
        }

        [Test]
        public void IsNotConnectedBool()
        {
            uut_.StopCharge();
            usbCharger_.Connected.Equals(false);
        }

        /***    CurrentState test     ***/
        [Test]
        public void CurrentValue_Fivehundred()
        {
            uut_.StartCharge();
            usbCharger_.CurrentValue.CompareTo(500);
        }

        [Test]
        public void CurrentValue_two_and_a_half()
        {
            uut_.StopCharge();
            usbCharger_.CurrentValue.CompareTo(0.0);
        }

        /***    Event called Test   ***/

        [Test]
        public void eventCalledDoneTime()
        {
            usbCharger_.CurrentValueEvent += (o, e) => { eventCount++; };
        }

        [Test]
        public void settingCurrentevent()
        {
            eventArgs.Current = 1;
            Assert.That(eventArgs.Current, Is.EqualTo(1));
        }

        [Test]
        public void eventCalled1Time()
        {
            usbCharger_.ReceivedWithAnyArgs(eventCount);
        }

        [Test]
        public void EventNotReceived()
        {
            usbCharger_.DidNotReceiveWithAnyArgs();
        }

     

        /***    Display with charge control   ***/
        [Test]
        public void displayIsConnected()
        {
            uut_.IsConnected();
            displaySimulator.Received(1).ConnectPhone();
        }

        [Test]
        public void displayPhoneStartCharging()
        {
            uut_.StartCharge();
            displaySimulator.Received(1).PhoneStartCharging();
        }

        [Test]
        public void displayStopCharging()
        {
            uut_.StopCharge();
            displaySimulator.Received(1).DisconnectPhone();
        }

    }
}
