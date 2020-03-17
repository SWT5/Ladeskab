using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;

namespace Ladeskab.Implementation
{
    public class Door : IDoor
    {
        public bool DoorState { get; private set; } 

        public event EventHandler DoorOpenEvent;

        public event EventHandler DoorCloseEvent;

        public void LockDoor()
        {
            Console.WriteLine("Door: Locked");
        }

        public void UnlockDoor()
        {
            Console.WriteLine("Door: unlocked");
        }

        protected virtual void OnDoorOpened()
        {
            DoorOpenEvent?.Invoke(this,null);
        }

        protected virtual void OnDoorClosed()
        {
            DoorCloseEvent?.Invoke(this,null);
        }

        // simuleringer til at klade event DoorOpenEvent
        public void SimulateDoorOpens()
        {
            Console.WriteLine("User Open door");
            DoorState = true;
            OnDoorOpened();
        }

        // simulering til at kalde event DoorCloseEvent
        public void SimulateDoorCloses()
        {
            Console.WriteLine("User closes door");
            DoorState = false;
            OnDoorClosed();
        }

        
    }
}
