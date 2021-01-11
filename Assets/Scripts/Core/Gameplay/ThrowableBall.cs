using Lean.Pool;
using SplitSpheres.Framework.ColorManagement;
using SplitSpheres.Framework.GameEvents.Events;
using SplitSpheres.Framework.ThrowablesSystem.Scripts;
using UnityEngine;

namespace SplitSpheres.Core.Gameplay
{
    public class ThrowableBall : Throwable, IPoolable
    {
        
        [SerializeField] private VoidEvent onArrivalEvent;

        [SerializeField] private CmColor32 _assignedCmColor32;

        public CmColor32 AssignedCmColor32
        {
            get => _assignedCmColor32;
            set => _assignedCmColor32 = value;
        }

        public override void OnArrival()
        {
            onArrivalEvent.Raise();
          
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Cylinder")) return;
            
            var cyl = other.gameObject.GetComponent<Cylinder>();

            if (_assignedCmColor32.CompareColor(cyl.AssignedCmColor32))
            {
                cyl.ProcessBallCollision();
            }
            else
            {
                AnimateBallGoAway();
            }
        }

        private void AnimateBallGoAway()
        {
            //TODO: AnimateBallAway with a random direction, speed and accel
            Debug.Log("TODO: BALL GO AWAY ANIMATION");
        }

        public void OnSpawn()
        {
           //Generate a color?
        }

        public void OnDespawn()
        {
         
        }
    }
}
