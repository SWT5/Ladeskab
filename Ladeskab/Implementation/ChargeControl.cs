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
        private readonly IUsbCharger _usbCharger;
        private IDisplay _display;



        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            _usbCharger = usbCharger;
            _display = display;
            _usbCharger.CurrentValueEvent += HandleCurrentChangedEvent;
            
        }

        private void HandleCurrentChangedEvent(object sender, CurrentEventArgs e)
        {
            CurrentCharge = e.Current;

            if (CurrentCharge == 0)
            {
                _display.WriteLine("Ingen Telefonen tilsuttet - oplader ikke...  aktuel ladestroem: " + CurrentCharge);
            }
            else if (CurrentCharge < 0 && CurrentCharge >= 5)
            {
                _display.WriteLine("Telefonen er fuldt opladt...  aktuel ladestroem: " + CurrentCharge);
            }
            else if (CurrentCharge > 5 && CurrentCharge <= 500)
            {
                _display.WriteLine("Telefonen oplader...  aktuel ladestroem: " + CurrentCharge);
            }
            else if (CurrentCharge > 500)
            {
                _display.WriteLine("FEJL!!!! Der er noget galt med ladestrømmen - frakobl telefonen...  aktuel ladestroem: " + CurrentCharge);
            }

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
