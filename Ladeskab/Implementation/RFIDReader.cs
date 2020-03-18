using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;

namespace Ladeskab.Implementation
{
    class RFIDReader : IRFIDReader
    {
        private string _oldId;
        public event EventHandler<RfidDetectedEventArgs> RfidDetectedEvent;

        public void RegisterId(string receivedChar)
        {
            RfidReceived(new RfidDetectedEventArgs{Id = receivedChar});
            _oldId = receivedChar;
        }

        protected virtual void RfidReceived(RfidDetectedEventArgs e)
        {
            RfidDetectedEvent?.Invoke(this,e);
        }
    }
}
