using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Ladeskab.Interfaces;
using Ladeskab.Simulator;


namespace Ladeskab.Implementation
{
    public class Door : IDoor
    {
        // DoorState True = door is closed, false = door is open
        public bool DoorState { get; private set; } 
        // LockState true = locked, false = unlocked
        public bool LockState { get; private set; } 

        public event EventHandler DoorOpenEvent;

        public event EventHandler DoorCloseEvent;

        public Door()
        {
            DoorState = true;   // door is closed
            LockState = false;  // door is unlocked
        }


        public void LockDoor()
        {
            LockState = true;
            Console.WriteLine("Door: Locked");
        }

        public void UnlockDoor()
        {
            LockState = false;
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

        // simuleringer til at kalde event DoorOpenEvent
        public void SimulateDoorOpens()
        {
            if (!LockState && DoorState)    // door is unlocked and closed
            {
                Console.WriteLine("User Opens door");
                DoorState = false; //Door is open 
                OnDoorOpened();
            }
            else if (LockState)
            {
                Console.WriteLine("User cant open door, cause its locked");
            }
            else
                Console.WriteLine("Door already open");
        }

        // simulering til at kalde event DoorCloseEvent
        public void SimulateDoorCloses()
        {
            if (!DoorState) // door is not already closed
            {
                Console.WriteLine("User closes door");
                DoorState = true; //Door is closed 
                OnDoorClosed();
            }
            else
                Console.WriteLine("door is already closed");
        }

        
    }
}
