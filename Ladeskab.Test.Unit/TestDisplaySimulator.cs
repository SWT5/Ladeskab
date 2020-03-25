using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;
using Ladeskab.Simulator;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    class TestDisplaySimulator
    {
        private IDisplay _uut;
        [SetUp]
        public void setup()
        {
            _uut = new DisplaySimulator();
        }

        [Test]
        public void ConnectionError_ConsoleOutput_IsTrue()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            _uut.ConnectionError();
            Assert.That(output.ToString(), Is.EqualTo("Din telefon er ikke ordentlig tilsluttet. Prøv igen.\r\n"));
        }

        [Test]
        public void ConnectPhone_ConsoleOutput_IsTrue()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            _uut.ConnectPhone();
            Assert.That(output.ToString(), Is.EqualTo("Tilslut telefon\r\n"));
        }

        [Test]
        public void PhoneStartCharging_ConsoleOutput_IsTrue()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            _uut.PhoneStartCharging();
            Assert.That(output.ToString(), Is.EqualTo("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.\r\n"));
        }

        [Test]
        public void DisconnectPhone_ConsoleOutput_IsTrue()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            _uut.DisconnectPhone();
            Assert.That(output.ToString(), Is.EqualTo("Tag din telefon ud af skabet og luk døren\r\n"));
        }

        [Test]
        public void WrongRfid_ConsoleOutput_IsTrue()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            _uut.WrongRfid();
            Assert.That(output.ToString(), Is.EqualTo("Forkert RFID tag\r\n"));
        }

        [Test]
        public void LoadRfid_ConsoleOutput_IsTrue()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            _uut.LoadRfid();
            Assert.That(output.ToString(), Is.EqualTo("Indlæs RFID\r\n"));
            
        }

        [Test]
        public void NoPhoneConnected_ConsoleOutput_IsTrue()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            _uut.NoPhoneConnected();
            Assert.That(output.ToString(), Is.EqualTo("Ingen telefon forbundet\r\n"));
        }
    }
}
