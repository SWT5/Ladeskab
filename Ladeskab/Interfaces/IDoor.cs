using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Interfaces
{
    public interface IDoor
    {
        bool DoorState { get; }
        bool LockState { get; }

        event EventHandler DoorOpenEvent;
        event EventHandler DoorCloseEvent;

        void LockDoor();
        void UnlockDoor();

        void SimulateDoorOpens();
        void SimulateDoorCloses();
    }
}
