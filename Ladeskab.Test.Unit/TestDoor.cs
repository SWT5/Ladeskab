using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Implementation;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    class TestDoor
    {
        private Door uut_;

        [SetUp]
        public void Setup()
        {
            uut_ = new Door();
            //uut_.UnlockDoor();
        }

        // State-Based-Tests door

        [Test]
        public void unlockDoor_doorIsUnlocked_LockState_Equal_False()
        {
            uut_.UnlockDoor();
            Assert.That(uut_.LockState,Is.False);
        }

        [Test]
        public void lockDoor_doorIsLocked_LockState_Equal_true()
        {
            uut_.LockDoor();
            Assert.That(uut_.LockState,Is.True);
        }

        [Test]
        public void simulateDoorOpens_doorIsOpen_doorState_Equal_false()
        {
            uut_.SimulateDoorOpens();
            Assert.That(uut_.DoorState,Is.False);
        }

        [Test]
        public void simulateDoorCloses_doorIsClosed_doorState_equal_true()
        {
            uut_.SimulateDoorCloses();
            Assert.That(uut_.DoorState,Is.True);
        }


        // interaction/ Behavior -based test
        [Test]
        public void simulateDoorOpens_DoorOpenEvent_isTriggered()
        {
            // hvordan tester man lige events og at det bliver fired? 
            uut_.SimulateDoorOpens();
        }


    }
}
