﻿using System;
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
            Assert.That(uut_.DoorState,Is.EqualTo(false));
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
