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
        private int currentActiveBalls = 0;

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

        public int CurrentActiveBalls
        {
            get => currentActiveBalls;
            set => currentActiveBalls = value;
        }


        public void SpawnThrowables(int numberOfBalls, CmColor32[] ballColors, LevelState currentLevelState)
        {
            CurrentActiveBalls = numberOfBalls;
            InitializedBallColors = ballColors;
            currentActiveLevelState = currentLevelState;
            UpdateBallsUI();
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

        /// <summary>
        /// Initializes the this Manager
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            CanThrowNewBall = true;
        }

        /// <summary>
        /// Raises tje number of Active balls
        /// </summary>
        /// <param name="newNBalls"> number of balls to add</param>
        public void GiveMoreBalls(int newNBalls)
        {
            CanThrowNewBall = true;
            CurrentActiveBalls = newNBalls;
            UpdateBallsUI();
        }

        private void ReduceNumberOfBalls()
        {
            CurrentActiveBalls--;
            ThrowablePool.Despawn(LastThrowable.gameObject);
            UpdateBallsUI();
            //check for no balls
            if (CurrentActiveBalls <= 0)
            {
                CanThrowNewBall = false;

                currentActiveLevelState.CheckForGameOver();
            }
        }

        private void UpdateBallsUI()
        {
            currentActiveLevelState.GameCanvasController.ballThrowablePanel.DisplayNumberOfBalls(CurrentActiveBalls);
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
            ball.GetComponent<ThrowableBall>()
                .AssignCmColor32(InitializedBallColors[RandomInt.GenerateNumber(0, InitializedBallColors.Length)]);
            return ball;
        }
    }
}