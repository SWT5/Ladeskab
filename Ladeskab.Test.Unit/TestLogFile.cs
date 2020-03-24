using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Implementation;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    class TestLogFile
    {
        private LogFile _uut;
        private string _id;
        private string _date;
        private string _state;

        [SetUp]
        public void setup()
        {   
            
            _uut = new LogFile();
            
        }

        [Test]
        public void LogDoorLocked_IsWorking()
        {
            bool temp = false;
            _uut.LogDoorLocked("1");
            _id = _uut.listOfData[0].Id;
            _date = _uut.listOfData[0].Date;
            _state = _uut.listOfData[0].State;
            if (_id == "1" && _date == DateTime.Now.ToString("dd/MM/yyy") && _state == "Door Locked")
                temp = true;
            Assert.That(temp, Is.EqualTo(true));
        }

        [Test]
        public void LogDoorUnLocked_IsWorking()
        {
            bool temp = false;
            _uut.LogDoorUnlocked("2");
            _id = _uut.listOfData[0].Id;
            _date = _uut.listOfData[0].Date;
            _state = _uut.listOfData[0].State;
            if (_id == "2" && _date == DateTime.Now.ToString("dd/MM/yyy") && _state == "Door Unlocked")
                temp = true;
            Assert.That(temp, Is.EqualTo(true));
        }

        [Test]
        public void LogError_IsWorking()
        {
            bool temp = false;
            _uut.LogError("Fejl");
            _id = _uut.listOfData[0].Id;
            _date = _uut.listOfData[0].Date;
            _state = _uut.listOfData[0].State;
            if (_id == "Error" && _date == DateTime.Now.ToString("dd/MM/yyy") && _state == "Fejl")
                temp = true;
            Assert.That(temp, Is.EqualTo(true));
        }
    }
}
