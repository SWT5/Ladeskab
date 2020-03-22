using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Implementation;
using Ladeskab.Interfaces;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    
    class TestRFIDreader
    {
        private int eventCount { get; set; } //variable to check if this event has been called 
        private RFIDReader _uut;
        private string Stringreceived { get; set; }

        [SetUp]
        public void Setup()
        {
            _uut = new RFIDReader();

            _uut.RfidDetectedEvent += (o, e) => { eventCount++; }; //lambda funktion (anonym funktion) - susanne
            _uut.RfidDetectedEvent += (o, e) => { Stringreceived = e.Id; };
        }

        [Test]
        public void Check_If_event_is_triggering_all_subscribers()
        {
            _uut.RegisterId("hej"); //use method to push an event 
            Assert.That(eventCount, Is.EqualTo(1)); //check if an event has happened 
        }

        [Test]
        public void IdProperty_Ischanged()
        {
            _uut.RegisterId("1");
            Assert.That(Stringreceived, Is.EqualTo("1"));
        }

    }
}
