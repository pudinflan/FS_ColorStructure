using System.Collections;
using System.Collections.Generic;
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
        
        private List<int> checkedActiveIndexes = new List<int>();
        
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
            LevelObjectRow.onRowEmpty -= ActivateNextRow;
        }
        
        private void ActivateLevel()
        {
            LevelObjectRow.onRowEmpty += ActivateNextRow;
            
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

            cameraManager.InitializeCamLevelState(); //TODO: CHANGE TO STATE SYSTEM
            
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
                cylRows[i].RowIndex = i;

                //Debug.Log(cylRows[i].name +"index: " + i);
             
                yield return new WaitForSeconds(.1f);
            }

          
                
            for (var i = cylRows.Length - 1; i >= nOfDeActiveRows; i--)
            {
                cylRows[i].RowIndex = i;
                cylRows[i].ActivateCyls();
            }
        }
        

        private void ActivateNextRow(int receivedIndex, Vector3 rowPosition)
        {
            if (checkedActiveIndexes.Contains(receivedIndex)) return;
            
            Debug.Log("Activate Next: "+receivedIndex);
            var cylRows = levelObjectInstance.cylRows;
            
            var calculatedIndex = receivedIndex -(cylRows.Length - receivedPreparedLevel.Level.numberOfInactiveRows);
            
            if (calculatedIndex < 0) return;
            cylRows[calculatedIndex].ActivateCyls();
            checkedActiveIndexes.Add(receivedIndex);
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