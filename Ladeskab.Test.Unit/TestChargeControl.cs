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

        [Test]
        public void startCharging()
        {
            //Act
            uut_.StartCharge();
            //Assert
            usbCharger_.Received(1).StartCharge();

        }

        [Test]
        public void stopCharging()
        {
            uut_.StopCharge();
            usbCharger_.Received(1).StopCharge();       // received 1 call for stopCharge
        }
    }
}
