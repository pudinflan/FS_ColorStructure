using Lean.Pool;
using SplitSpheres.Framework.ColorManagement;
using SplitSpheres.Framework.GameEvents.Events;
using SplitSpheres.Framework.ThrowablesSystem.Scripts;
using UnityEngine;

namespace SplitSpheres.Core.Gameplay
{
    public class ThrowableBall : Throwable, IPoolable
    {
        [Header("Color Management")]
        [SerializeField] private MeshRenderer mr;
        [SerializeField] private CmColor32 assignedCmColor32;
        
        [Header(("Events"))]
        [SerializeField] private VoidEvent onArrivalEvent;

        public CmColor32 AssignedCmColor32 => assignedCmColor32;
        
        public void AssignCmColor32(CmColor32 cmColor32ToAssign)
        {
            assignedCmColor32 = cmColor32ToAssign;
            mr.material = cmColor32ToAssign.cmColor32Material;
        }

        public override void OnArrival()
        {
            onArrivalEvent.Raise();
          
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Cylinder")) return;
            
            var cyl = other.gameObject.GetComponent<Cylinder>();

            if (assignedCmColor32.CompareColor(cyl.AssignedCmColor32.colorTag))
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
