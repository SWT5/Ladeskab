using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ladeskab.Implementation;
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



        [SetUp]
        public void Setup()
        {
            usbCharger_ = Substitute.For<IUsbCharger>();
            uut_ = new ChargeControl(usbCharger_);
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

        /***Event called Test***/

        [Test]
        public void eventCalledDoneTime()
        {
            uut_=new ChargeControl(usbCharger_);
            usbCharger_.CurrentValueEvent += (o, e) => { eventCount++; };
        }

        [Test]
        public void eventCalled1Time()
        {
            uut_ = new ChargeControl(usbCharger_);
            usbCharger_.ReceivedWithAnyArgs(eventCount);
        }




    }
}
