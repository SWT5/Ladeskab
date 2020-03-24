using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters;

namespace Ladeskab.Implementation
{
    public class LogFileData
    {
        private string _date;
        private string _id;
        private string _state;

        public LogFileData(string id, string date, string state)
        {
            _id = id;
            _date = date;
            _state = state; 
        }

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }



    }
}
