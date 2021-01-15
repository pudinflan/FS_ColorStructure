using System.Collections;
using System.Collections.Generic;
using SplitSpheres.Core.Gameplay;
using SplitSpheres.Core.GUI;
using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.SimpleFSM;
using SplitSpheres.General;
using UnityEngine;

namespace SplitSpheres.Core.GameStates
{
    public class LevelState : MonoBehaviour, IState
    {
        public readonly GameCanvasController GameCanvasController;

        private readonly PreparedLevel receivedPreparedLevel;
        private readonly LevelObject levelObjectInstance;

        private readonly GameManager gameManager;
        private readonly CameraManager cameraManager;
        private readonly BallThrowableManager ballThrowableManager;

        private readonly List<int> checkedActiveIndexes = new List<int>();

        private int totalCyls;
        private int destroyedCyls;

        private bool canLose = true;
        private bool alreadyLostOnce = false;

        public LevelState(PreparedLevel preparedLevel, GameManager gameManager)
        {
            receivedPreparedLevel = preparedLevel;
            levelObjectInstance = preparedLevel.PreparedLevelGOInstance.GetComponent<LevelObject>();

            this.gameManager = gameManager;
            cameraManager = gameManager.cameraManager;
            ballThrowableManager = gameManager.ballThrowableManager;
            GameCanvasController = gameManager.gameCanvasController;
        }

        public GameManager Manager => gameManager;

        public bool CanLose
        {
            get => canLose;
            set => canLose = value;
        }

        public void Enter()
        {
            //activate Level object
            levelObjectInstance.gameObject.SetActive(true);
        }


        public void Execute()
        {
        }

        public void Exit()
        {
            LevelObjectRow.ONRowEmpty -= ActivateNextRow;
            LevelObjectRow.ONCylDropped -= DecreaseNumberOfCyls;
            Cylinder.onCylDestroyed -= DecreaseNumberOfCyls;
        }


        private void WinState()
        {
            CanLose = false;
            ballThrowableManager.CanThrowNewBall = false;
            Manager.GameStateMachine.ChangeState(new WinState(this, receivedPreparedLevel.Level));
        }

        private void GameOverState()
        {
            if (!CanLose) return;
            if (alreadyLostOnce)
            {
                Manager.GameStateMachine.ChangeState(new GameOverState(this, receivedPreparedLevel.Level));
            }
            else
            {
                alreadyLostOnce = true;
                Manager.mainCanvasController.retryGamePanel.ShowRetry(this, receivedPreparedLevel.Level);
            }
        }

        public void CheckForGameOver()
        {
            Manager.StartCoroutine(CheckForGameOverRoutine());
        }


        public void ActivateLevel()
        {
            LevelObjectRow.ONRowEmpty += ActivateNextRow;
            LevelObjectRow.ONCylDropped += DecreaseNumberOfCyls;
            Cylinder.onCylDestroyed += DecreaseNumberOfCyls;


            //Starts the level Sequence
            Manager.StartCoroutine(ActivateLevelSequence());
        }


        private IEnumerator ActivateLevelSequence()
        {
            var cameraTarget = levelObjectInstance.cylRows[levelObjectInstance.cylRows.Length - 1].transform.position;
            var accel = 4f;

            while (!cameraManager.SmoothMoveToPosAndRotate(cameraTarget, accel))
            {
                accel -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            cameraManager.InitializeCamLevelState(); //TODO: CHANGE TO STATE SYSTEM

            var numberOfRowsToDeActivate = receivedPreparedLevel.Level.numberOfInactiveRows;

            Manager.StartCoroutine(InitializeRows(numberOfRowsToDeActivate));

            GameCanvasController.TurnOnGameCanvas(true);
            //Initialize BallTrhowable System
            InitializeBallThrowable();
        }

        private IEnumerator InitializeRows(int nOfDeActiveRows)
        {
            totalCyls = 0;
            var cylRows = levelObjectInstance.cylRows;

            //Deactivates Cyls that need to be Hidden
            for (var i = nOfDeActiveRows - 1; i >= 0; i--)
            {
                cylRows[i].DeactivateCyls();
                cylRows[i].RowIndex = i;


                foreach (var cyl in cylRows[i].rowOfCylinders)
                {
                    totalCyls++;
                }

                yield return new WaitForSeconds(.1f);
            }

            for (var i = cylRows.Length - 1; i >= nOfDeActiveRows; i--)
            {
                cylRows[i].RowIndex = i;
                cylRows[i].ActivateCyls();


                foreach (var cyl in cylRows[i].rowOfCylinders)
                {
                    totalCyls++;
                }
            }

            destroyedCyls = 0;
            //Initialize progress bar
            GameCanvasController.progressBar.Progress(totalCyls, 0);
        }


        private void InitializeBallThrowable()
        {
            //Activate Balls
            ballThrowableManager.SpawnThrowables(receivedPreparedLevel.Level.numberOfBalls,
                receivedPreparedLevel.Level.cmColor32S, this);
        }
        
        
        private void ActivateNextRow(int receivedIndex, Vector3 rowPosition)
        {
            if (checkedActiveIndexes.Contains(receivedIndex)) return;


            var cylRows = levelObjectInstance.cylRows;
            var activeRowsCount = (cylRows.Length - receivedPreparedLevel.Level.numberOfInactiveRows);
            var calculatedIndex = receivedIndex - activeRowsCount;


            if (calculatedIndex < 0) return;

            for (var i = calculatedIndex; i < cylRows.Length  - activeRowsCount; i++)
            {
                var cylRow = cylRows[i];
                if (cylRow.AlreadyActivated) continue;
                cylRow.ActivateCyls();
            }

            checkedActiveIndexes.Add(receivedIndex);
        }


        private void DecreaseNumberOfCyls()
        {
            destroyedCyls++;
            GameCanvasController.progressBar.Progress(totalCyls, destroyedCyls);
            CheckForEndOfLevel();
        }

        private void CheckForEndOfLevel()
        {
            if (destroyedCyls >= totalCyls)
            {
                WinState();
            }
        }


        private IEnumerator CheckForGameOverRoutine()
        {
            float time = 0;

            while (time < 4f)
            {
                time += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            GameOverState();
        }


        public void OnDestroy()
        {
            GameCanvasController.TurnOnGameCanvas(false);
            Destroy(levelObjectInstance.gameObject);
        }
    }
}