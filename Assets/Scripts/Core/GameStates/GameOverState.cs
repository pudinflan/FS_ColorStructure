using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.SimpleFSM;
using UnityEngine;

namespace SplitSpheres.Core.GameStates
{
    public class GameOverState : MonoBehaviour, IState
    {
        private readonly MainCanvasController mainCanvasController;
        private readonly Level previousLevel;
        private readonly LevelState previousLevelState;

        public GameOverState(LevelState levelState, Level level)
        {
            previousLevelState = levelState;
            mainCanvasController = levelState.Manager.mainCanvasController;
            previousLevel = level;
        }

        public void Enter()
        {
            mainCanvasController.endGamePanel.RewardAndShow(previousLevel.numberOfBalls +
                                                            previousLevelState.Manager.ballThrowableManager
                                                                .CurrentActiveBalls, false);
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