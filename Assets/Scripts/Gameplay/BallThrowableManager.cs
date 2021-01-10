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
            _canThrowNewBall = true;
            ThrowablePool.Despawn(LastThrowable.gameObject);
        }
        
        public override void ThrowThrowable(Transform targetTransform)
        {
            if(!_canThrowNewBall)
                return;
            _canThrowNewBall = false;
            
            var ballToThrow = MainThrowable;
            ballToThrow.Throw(targetTransform.position);
            
            UpdateThrowables();
        }
    }
}
