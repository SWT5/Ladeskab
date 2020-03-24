using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace Ladeskab.Test.Unit
{
    public class FakeChargecontrol : IChargeControl
    {
        public bool IsConnected()
        {
            return true;
        }

        public void StartCharge()
        {
            //do nothing 
        }

        public void StopCharge()
        {
            //do nothing
        }
    }
}
