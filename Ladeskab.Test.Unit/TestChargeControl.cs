using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Implementation;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;

namespace Ladeskab.Test.Unit
{
    class TestChargeControl
    {
        private ChargeControl uut_;
        public bool eventCalled { get; set; }
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

        /***    CurrentState test     ***/
        [Test]
        public void CurrentValue_FiveHundred()
        {

        }

        [Test]
        public void CurrentValue_two_And_a_Half()
        {

        }

        [Test]
        public void overloaded()
        {

        }
    }
}
