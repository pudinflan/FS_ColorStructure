using SplitSpheres.Core.Gameplay;
using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.SimpleFSM;
using UnityEngine;

namespace SplitSpheres.Core.GameStates
{
    public class LevelState : MonoBehaviour, IState
    {
        private readonly PreparedLevel receivedPreparedLevel;
        private readonly BallThrowableManager ballThrowableManager;

        public  LevelState(PreparedLevel preparedLevel, BallThrowableManager ballThrowableManager)
        {
            receivedPreparedLevel = preparedLevel;
            this.ballThrowableManager = ballThrowableManager;
        }
        
        public void Enter()
        {
            //ActivateLevelObject
            receivedPreparedLevel.PreparedLevelGOInstance.SetActive(true);
            ballThrowableManager.InitializedBallColors = receivedPreparedLevel.Level.cmColor32S;
            ballThrowableManager.Initialize();
        }

        public void Execute()
        {
         
        }

        public void Exit()
        {
          
        }

        public void OnDestroy()
        {
           
        }
    }
}
