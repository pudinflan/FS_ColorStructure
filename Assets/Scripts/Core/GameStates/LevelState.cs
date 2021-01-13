using System.Collections;
using SplitSpheres.Core.Gameplay;
using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.SimpleFSM;
using SplitSpheres.General;
using UnityEngine;

namespace SplitSpheres.Core.GameStates
{
    public class LevelState : MonoBehaviour, IState
    {
   
        private readonly PreparedLevel receivedPreparedLevel;
        private readonly LevelObject levelObjectInstance;

        private readonly GameManager gameManager;
        private readonly CameraManager cameraManager;
        private readonly BallThrowableManager ballThrowableManager;
        
        public LevelState(PreparedLevel preparedLevel, GameManager gameManager)
        {
            receivedPreparedLevel = preparedLevel;
            levelObjectInstance = preparedLevel.PreparedLevelGOInstance.GetComponent<LevelObject>();

            this.gameManager = gameManager;
            cameraManager = gameManager.cameraManager;
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

           
 
       
            //Starts the level Sequence
            gameManager.StartCoroutine(ActivateLevelSequence());
            
            //InitializeRows
            //gameManager.StartCoroutine());
        }
        


        private IEnumerator ActivateLevelSequence()
        {
            var cameraTarget = levelObjectInstance.cylRows[levelObjectInstance.cylRows.Length -1].transform.position;
            var accel = 4f;
            
            while (!cameraManager.SmoothMoveToPosAndRotate(cameraTarget, accel))
            {
                accel -= Time.deltaTime;
                
                yield return new WaitForEndOfFrame();
            }

            var numberOfRowsToDeActivate = receivedPreparedLevel.Level.numberOfInactiveRows;
            
            gameManager.StartCoroutine(InitializeRows(numberOfRowsToDeActivate));
            
            //Initialize BallTrhowable System
            InitializeBallThrowable();
        }

        private IEnumerator InitializeRows(int nOfDeActiveRows)
        {
            var cylRows = levelObjectInstance.cylRows;
            //Deactivates Cyls that need to be Hidden
            for (var i = nOfDeActiveRows - 1; i >= 0; i--)
            {
                cylRows[i].DeactivateCyls();
           
                yield return new WaitForSeconds(.1f);
            }

            for (var i = cylRows.Length - 1; i >= nOfDeActiveRows; i--)
            {
                cylRows[i].ActivateCyls();
            }
        }

        
        private void InitializeBallThrowable()
        {
            //Activate Balls
            ballThrowableManager.SpawnThrowables(receivedPreparedLevel.Level.numberOfBalls,
                receivedPreparedLevel.Level.cmColor32S);
        }

        public void OnDestroy()
        {
            Destroy(levelObjectInstance.gameObject);
        }
    }
}