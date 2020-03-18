using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Implementation;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    
    class TestRFIDreader
    {
        private int eventCount { get; set; } //variable to check if this event has been called 
        private RFIDReader _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new RFIDReader();

            _uut.RfidDetectedEvent += (o, e) => { eventCount++; }; //lambda funktion (anonym funktion) - susanne
        }

        [Test]
        public void increment()
        {
            _uut.RegisterId("hej"); //use method to push an event 
            Assert.That(eventCount, Is.EqualTo(1)); //check if an event has happened 
        }
    }
}
