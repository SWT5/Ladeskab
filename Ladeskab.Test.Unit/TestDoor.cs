using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Implementation;
using Ladeskab.Interfaces;
using NSubstitute;
using NUnit.Framework;




namespace Ladeskab.Test.Unit
{
    class TestDoor
    {
        private Door uut_;
        public bool eventCalled { get; set; }
        public int eventCalledCount { get; set; }
        //private IDoor door_;

        [SetUp]
        public void Setup()
        {
            //door_ = Substitute.For<IDoor>();
            uut_ = new Door();
            
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
            Assert.That(uut_.LockState,Is.EqualTo(true));
        }

        [Test]
        public void simulateDoorOpens_doorIsOpen_doorState_Equal_false()
        {
            uut_.SimulateDoorOpens();
            Assert.That(uut_.DoorState,Is.EqualTo(false));
        }

        [Test]
        public void simulateDoorCloses_doorIsClosed_doorState_equal_true()
        {
            uut_.SimulateDoorCloses();
            Assert.That(uut_.DoorState,Is.EqualTo(true));
        }


        // interaction/ Behavior -based test

        // vil teste at OpenDoorEvent bliver fired
        [Test]
        public void simulateDoorOpens_DoorOpenEvent_isFired()
        {

            // hvordan tester man lige events og at det bliver fired? - er dette så rigtigt? 
            uut_.DoorOpenEvent += (Object,e) => eventCalled = true;

            uut_.SimulateDoorOpens();

            Assert.That(eventCalled,Is.EqualTo(true));
        }

        [Test]
        public void simulateDoorOpens_DoorLocked_DoorOpenEvent_isNotFired()
        {
            uut_.DoorOpenEvent += (Object,e) => eventCalled = true;
            uut_.LockDoor();
            uut_.SimulateDoorOpens();
            Assert.That(eventCalled,Is.EqualTo(false));
        }

        [Test]
        public void simulateDoorOpens_DoorAlreadyOpened_DoorOpenEvent_isNotFiredTwice()
        {
            uut_.DoorOpenEvent += (Object, e) => eventCalledCount++;
            uut_.SimulateDoorOpens();
            uut_.SimulateDoorOpens();
            Assert.That(eventCalledCount,Is.EqualTo(1));
        }

        // test that you can close door if door is open, and 
        [Test]
        public void simulateDoorCloses_DoorClosesEvent_isFired()
        {
            uut_.DoorCloseEvent += (Object, e) => eventCalled = true;
            uut_.SimulateDoorOpens();
            uut_.SimulateDoorCloses();
            Assert.That(eventCalled,Is.EqualTo(true));
        }

        // test that you cant close door if its already closed 
        [Test]
        public void simulateDoorCloses_DoorAlreadyClosed_DoorCloseEvent_isNotFired()
        {
            uut_.DoorCloseEvent += (Object, e) => eventCalled = true; 
            uut_.SimulateDoorCloses();
            Assert.That(eventCalled, Is.EqualTo(false));
        }
    }
}
