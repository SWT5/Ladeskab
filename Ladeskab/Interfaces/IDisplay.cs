using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Interfaces
{

    public interface IDisplay
    {
        void ConnectPhone();
        void LoadRfid();
        void ConnectionError();
        void WrongRfid();
        void DisconnectPhone();
        void PhoneStartCharging();
        void CloseDoorToReadRfid();
    }
}
