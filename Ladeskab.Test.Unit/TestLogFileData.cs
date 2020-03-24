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
    class TestLogFileData
    {
        public LogFileData _uut;


        private string _id = "1";
        private string _date = "12/04";
        private string _state = "Locked";

        [SetUp]
        public void setup()
        {

            _uut = new LogFileData(_id,_date,_state);
        }

        [Test]
        public void check_if_id_IsEqualTo_1()
        {
            Assert.That(_uut.Id, Is.EqualTo("1"));
        }

        [Test]
        public void set_id_to_2()
        {
            _uut.Id = "2";
            Assert.That(_uut.Id, Is.EqualTo("2"));
        }

        [Test]
        public void check_if_date_IsEqualTo_1204()
        {
            Assert.That(_uut.Date, Is.EqualTo("12/04"));
        }

        [Test]
        public void set_date_to_1212()
        {
            _uut.Date = "12/12";
            Assert.That(_uut.Date, Is.EqualTo("12/12"));
        }

        [Test]
        public void check_if_state_IsEqualTo_Locked()
        {
            Assert.That(_uut.State, Is.EqualTo("Locked"));
        }

        [Test]
        public void set_state_to_Unlocked()
        {
            _uut.State = "Unlocked";
            Assert.That(_uut.State, Is.EqualTo("Unlocked"));
        }


    }
}
