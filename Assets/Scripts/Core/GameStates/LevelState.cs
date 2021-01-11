using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.SimpleFSM;
using UnityEngine;

namespace SplitSpheres.Core.GameStates
{
    public class LevelState : MonoBehaviour, IState
    {
        private Level initializedLevel;
        
        public  LevelState(Level levelToInitialize)
        {
            initializedLevel = levelToInitialize;
        }
        
        public void Enter()
        {
            //TODO: Romove Instantiate and chave to a load ObjectPool in GameManager
            GameObject.Instantiate(initializedLevel.LevelObject);
            
            Debug.Log(initializedLevel.numberOfBalls);
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
