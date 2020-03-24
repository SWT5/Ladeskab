using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;
using Ladeskab.Simulator;
using UsbSimulator;

namespace Ladeskab.Implementation
{
    public class ChargeControl : IChargeControl
    {
        public double CurrentCharge { get; set; }
        private IUsbCharger _usbCharger;
        DisplaySimulator Display_ = new DisplaySimulator();



        public ChargeControl(IUsbCharger usbCharger)
        {
            _usbCharger = usbCharger;
            _usbCharger.CurrentValueEvent += HandleCurrentChangedEvent;
            
        }

        private void HandleCurrentChangedEvent(object sender, CurrentEventArgs e)
        {
            CurrentCharge = e.Current;
        }
        
        public bool IsConnected()
        {
            return _usbCharger.Connected;
            Display_.ConnectPhone();
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
