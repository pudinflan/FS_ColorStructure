using System.Collections;
using SplitSpheres.Core.GameStates;
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
        private LevelState currentActiveLevelState;
        private int baseNumberOfBalls = 0;
        
        /// <summary>
        /// Can a new ball be thrown?
        /// </summary>
        private bool _canThrowNewBall;

        /// <summary>
        /// The possible colors of the balls. passed on level generation to make sure the balls have the same color present in the level
        /// </summary>
        public CmColor32[] InitializedBallColors { get; set; }

        /// <summary>
        /// Can a new ball be thrown?
        /// </summary>
        public bool CanThrowNewBall
        {
            get => _canThrowNewBall;
            set => _canThrowNewBall = value;
        }


        public void SpawnThrowables(int numberOfBalls,CmColor32[] ballColors, LevelState currentLevelState)
        {
            baseNumberOfBalls = numberOfBalls;
            InitializedBallColors = ballColors;
            currentActiveLevelState = currentLevelState;
             currentActiveLevelState.GameCanvasController.ballThrowablePanel.DisplayNumberOfBalls(baseNumberOfBalls);
            Initialize();
        }
        
        /// <summary>
        ///Called on the script GameObject via VoidListener component
        /// </summary>
        public void OnSphereArriveCylinder()
        {
            CanThrowNewBall = true;
         
            ReduceNumberOfBalls();
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
           
            CanThrowNewBall = true;
        }
        
        private void ReduceNumberOfBalls()
        {
            baseNumberOfBalls--;
            ThrowablePool.Despawn(LastThrowable.gameObject);
            currentActiveLevelState.GameCanvasController.ballThrowablePanel.DisplayNumberOfBalls(baseNumberOfBalls);
            //check for no balls
            if (baseNumberOfBalls <= 0)
            {
                CanThrowNewBall = false;

                currentActiveLevelState.CheckForGameOver();
            }
        }
        

        public override void ThrowThrowable(Vector3 position)
        {
            if (!CanThrowNewBall)
                return;

            var ballToThrow = MainThrowable;
            ballToThrow.Throw(position);

            UpdateThrowables();
            CanThrowNewBall = false;
            
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