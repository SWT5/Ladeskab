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
            IUsbCharger usbCharger = new UsbChargerSimulator();
            return usbCharger.Connected;
        }

        public void StartCharge()
        {
            IUsbCharger usbCharger = new UsbChargerSimulator();
            usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            IUsbCharger usbCharger= new UsbChargerSimulator();
            usbCharger.StopCharge();
        }
    }
}
