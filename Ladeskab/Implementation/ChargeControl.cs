using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace Ladeskab.Implementation
{
    public class ChargeControl : IChargeControl
    {
        public double CurrentCharge { get; set; }

        private readonly IUsbCharger _usbCharger;

        public ChargeControl()
        {
            _usbCharger = new UsbChargerSimulator();
        }

        public ChargeControl(IUsbCharger Currentstate)
        {
            Currentstate.CurrentValueEvent += HandleCurrentChangedEvent;
        }

        private void HandleCurrentChangedEvent(object sender, CurrentEventArgs e)
        {
            CurrentCharge = e.Current;
        }
        
        public bool IsConnected()
        {
            return _usbCharger.Connected;
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }
    }
}
