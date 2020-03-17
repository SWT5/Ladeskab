using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Implementation
{
    public class ChargeControl : IChargeControl
    {
        public event EventHandler<ChargeChangedEventArgs> ChargeChangedEvent;
        private bool _oldCharge;

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public void StartCharge(bool ChargeChanged)
        {
            if (ChargeChanged != _oldCharge)
            {
                OnChargeChanged(new ChargeChangedEventArgs{});
            }
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }

        protected virtual void OnChargeChanged(ChargeChangedEventArgs e)
        {
            ChargeChangedEvent?.Invoke(this,e);
        }
    }
}
