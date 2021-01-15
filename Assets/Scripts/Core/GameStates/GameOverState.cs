using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.SimpleFSM;
using UnityEngine;

namespace SplitSpheres.Core.GameStates
{
    public class GameOverState : MonoBehaviour, IState
    {
        private MainCanvasController mainCanvasController;
        private Level previousLevel;
        
        public GameOverState(LevelState levelState, Level level)
        {
            mainCanvasController = levelState.Manager.mainCanvasController;
            previousLevel = level;
        }
        
        public void Enter()
        {
            mainCanvasController.endGamePanel.RewardAndShow(previousLevel.numberOfBalls);
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
