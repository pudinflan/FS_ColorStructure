using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.SimpleFSM;
using UnityAdsIntegration;
using UnityEngine;

namespace SplitSpheres.Core.GameStates
{
    public class WinState : MonoBehaviour, IState
    {
        private MainCanvasController mainCanvasController;
        private Level previousLevel;
        private LevelState previousLevelState;

        public WinState(LevelState levelState, Level level)
        {
            previousLevelState = levelState;
            mainCanvasController = levelState.Manager.mainCanvasController;
            previousLevel = level;
        }
        
        public void Enter()
        {
            AdsManager.ShowVideoAd();
            mainCanvasController.endGamePanel.RewardAndShow(previousLevel.numberOfBalls +
                                                            previousLevelState.Manager.ballThrowableManager
                                                                .CurrentActiveBalls, true);
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