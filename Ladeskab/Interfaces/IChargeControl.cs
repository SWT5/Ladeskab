using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class ChargeChangedEventArgs : EventArgs
    {
        public bool charge { get; set; }
    }

    public interface IChargeControl
    {
        event EventHandler<ChargeChangedEventArgs> ChargeChangedEvent;
        bool IsConnected();
        void StartCharge();
        void StopCharge();


    }
}
