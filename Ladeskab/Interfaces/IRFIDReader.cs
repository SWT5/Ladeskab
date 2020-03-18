using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Interfaces
{
    public class RfidDetectedEventArgs : EventArgs
    {
        public string Id { get; set; } //Property to hold the event state 
    }

    public interface IRFIDReader
    {
        event EventHandler<RfidDetectedEventArgs> RfidDetectedEvent;
        void RegisterId(string receivedChar);
    }
}
