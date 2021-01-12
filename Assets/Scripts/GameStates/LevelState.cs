using System.Collections;
using SplitSpheres.Core.Gameplay;
using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.SimpleFSM;
using UnityEngine;

namespace SplitSpheres.GameStates
{
    public class LevelState : MonoBehaviour, IState
    {
   
        private readonly PreparedLevel receivedPreparedLevel;
        private readonly LevelObject levelObjectInstance;

        private readonly GameManager gameManager;
        private readonly BallThrowableManager ballThrowableManager;

        public LevelState(PreparedLevel preparedLevel, GameManager gameManager)
        {
            receivedPreparedLevel = preparedLevel;
            levelObjectInstance = preparedLevel.PreparedLevelGOInstance.GetComponent<LevelObject>();

            this.gameManager = gameManager;
            ballThrowableManager = gameManager.ballThrowableManager;
        }

        public void Enter()
        {
            //ActivateLevelObject
            ActivateLevel();
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }
        
        private void ActivateLevel()
        {
            //activate Level object
            levelObjectInstance.gameObject.SetActive(true);

            //Activate Balls
            ballThrowableManager.SpawnThrowables(receivedPreparedLevel.Level.numberOfBalls,
                receivedPreparedLevel.Level.cmColor32S);

       
            StartCoroutine(ActivateLevelSequence());
            
            //InitializeRows
            //gameManager.StartCoroutine(InitializeRows(receivedPreparedLevel.Level.numberOfInactiveRows));
        }
        
        
        public void OnDestroy()
        {
            Destroy(levelObjectInstance.gameObject);
        }

        private IEnumerator ActivateLevelSequence()
        {
            
        }
        
        
        private IEnumerator InitializeRows(int numberOfInitialRows)
        {
            var cylRows = levelObjectInstance.cylRows;
            //Deactivates Cyls that need to be Hidden
            for (var index = 0; index < numberOfInitialRows; index++)
            {
                var cylRow = cylRows[index];
                cylRow.DeactivateCyls();
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}