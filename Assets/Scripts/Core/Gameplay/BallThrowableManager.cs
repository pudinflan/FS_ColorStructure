using SplitSpheres.Framework.ColorManagement;
using SplitSpheres.Framework.GameEvents.Listeners;
using SplitSpheres.Framework.ThrowablesSystem.Scripts;
using SplitSpheres.Framework.Utils;
using UnityEngine;

namespace SplitSpheres.Core.Gameplay
{
    [RequireComponent(typeof(VoidListener), typeof(Vector3Listener))]
    public class BallThrowableManager : ThrowableManager
    {
        private bool _canThrowNewBall;

        public CmColor32[] InitializedBallColors { get; set; }
        

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
        
        public override void Initialize()
        {
            base.Initialize();
            _canThrowNewBall = true;
        }

        public override void ThrowThrowable(Vector3 position)
        {
            if (!_canThrowNewBall)
                return;

            var ballToThrow = MainThrowable;
            ballToThrow.Throw(position);

            UpdateThrowables();
            _canThrowNewBall = false;
        }
        
        protected override Throwable LoadThrowableIntoSpot(Transform throwSpot)
        {
            var ball = ThrowablePool.Spawn(throwSpot.position, Quaternion.identity, throwSpot)
                .GetComponent<Throwable>();
            ball.GetComponent<ThrowableBall>().AssignCmColor32(InitializedBallColors[RandomInt.GenerateNumber(0, InitializedBallColors.Length)]);
            return ball;
        }
    }
}