using System;
using SplitSpheres.Framework.GameEvents.Events;
using SplitSpheres.Framework.GameEvents.Listeners;
using SplitSpheres.Framework.ThrowablesSystem.Scripts;
using UnityEngine;

namespace SplitSpheres.Gameplay
{
    [RequireComponent(typeof(VoidListener))]
    public class BallThrowableManager : ThrowableManager
    {
        private bool _canThrowNewBall;
        
        private void Start()
        {
            Initialize();
            _canThrowNewBall = true;
        }

        //Called on the script GameObject via VoidListener component
        public void OnSphereArriveCylinder()
        {
            ThrowablePool.Despawn(LastThrowable.gameObject);
            _canThrowNewBall = true;
        }
        
        public override void ThrowThrowable(Transform targetTransform)
        {
            if(!_canThrowNewBall)
                return;
           
            
            var ballToThrow = MainThrowable;
            ballToThrow.Throw(targetTransform.position);
            
            UpdateThrowables();
            _canThrowNewBall = false;
        }
    }
}
