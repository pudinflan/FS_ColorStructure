using SplitSpheres.Core.GameStates;
using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.GUI.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SplitSpheres.Core.GUI.General
{
    public class GUIPanelRetry : GUIPanel
    {
        public GUIPanelRetryRewarded GUIPanelRetryRewarded;
            
        private LevelState savedLevelState;
        private Level savedLevel;

        protected override void Awake()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

            if (startActive) Show();

            GUIPanelRetryRewarded.enabled = false;
        }

        public override void Show()
        {
            base.Show();
            GUIPanelRetryRewarded.enabled = true;
        }

        public override void Hide()
        {
            base.Hide();
            GUIPanelRetryRewarded.enabled = false;
            savedLevelState.Manager.GameStateMachine.ChangeState(new GameOverState(savedLevelState, savedLevel));
        }

        public void ResumeGameWithBalls()
        {
            savedLevelState.CanLose = true;
         
            savedLevelState.Manager.ballThrowableManager.GiveMoreBalls(3);

          Close();
        }

        public void ShowRetry(LevelState levelState, Level level)
        {
            savedLevelState = levelState;
            savedLevel = level;
            
            Show();
        }

        private void Close()
        {
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0;
            }
            //GUIPanelRetryRewarded.enabled = false;
        }
    }
}