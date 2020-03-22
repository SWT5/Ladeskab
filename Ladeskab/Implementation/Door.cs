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
        // DoorState True = door is closed, false = door is open
        public bool DoorState { get; private set; } 
        // LockState true = locked, false = unlocked
        public bool LockState { get; private set; } 

        public event EventHandler DoorOpenEvent;

        public event EventHandler DoorCloseEvent;


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

        // simuleringer til at klade event DoorOpenEvent
        public void SimulateDoorOpens()
        {
            Console.WriteLine("User Opens door");
            DoorState = false; //Door is open 
            OnDoorOpened();
        }

        // simulering til at kalde event DoorCloseEvent
        public void SimulateDoorCloses()
        {
            Console.WriteLine("User closes door");
            DoorState = true; //Door is closed 
            OnDoorClosed();
        }

        
    }
}
