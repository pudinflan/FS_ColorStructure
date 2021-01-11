using System;
using SplitSpheres.Framework.ColorManagement;
using SplitSpheres.Framework.GameEvents.Events;
using SplitSpheres.Framework.GameEvents.Listeners;
using SplitSpheres.Framework.ThrowablesSystem.Scripts;
using UnityEngine;

namespace SplitSpheres.Gameplay
{
    [RequireComponent(typeof(VoidListener), typeof(Vector3Listener))]
    public class BallThrowableManager : ThrowableManager 
    {
        private bool _canThrowNewBall;
        
        private void Start()
        {
            Initialize();
            _canThrowNewBall = true;
        }
        
        /// <summary>
        ///Called on the script GameObject via VoidListener component
        /// </summary>
        public void OnSphereArriveCylinder()
        {
            _canThrowNewBall = true;
            ThrowablePool.Despawn(LastThrowable.gameObject);
            
            Debug.Log("Arrival event Received");
        }

        /// <summary>
        ///Called on the script GameObject via Vector3Listener component
        /// </summary>
        public void ThrowRequest(Vector3 position)
        {
            ThrowThrowable(position);
            Debug.Log("ThrowRequest event Received");
        }
        
        public override void ThrowThrowable(Vector3 position)
        {
            if(!_canThrowNewBall)
                return;
            
            var ballToThrow = MainThrowable;
            ballToThrow.Throw(position);
            
            UpdateThrowables();
            _canThrowNewBall = false;
        }
        
    }
}
