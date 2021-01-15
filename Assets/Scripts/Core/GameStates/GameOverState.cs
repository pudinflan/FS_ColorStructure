using SplitSpheres.Framework.SimpleFSM;
using UnityEngine;

namespace SplitSpheres.Core.GameStates
{
    public class GameOverState : MonoBehaviour, IState
    {
       
        public void Enter()
        {
      
            //TODO: OPEN HERE THE RETRY MENU
            Debug.Log("YOU LOOSE");
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
