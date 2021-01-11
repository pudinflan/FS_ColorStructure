using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.SimpleFSM;
using UnityEngine;

namespace SplitSpheres.Core.GameStates
{
    public class LevelState : MonoBehaviour, IState
    {
        private PreparedLevel receivedPreparedLevel;
        
        public  LevelState(PreparedLevel preparedLevel)
        {
            receivedPreparedLevel = preparedLevel;
        }
        
        public void Enter()
        {
            //ActivateLevelObject
            receivedPreparedLevel.PreparedLevelGOInstance.SetActive(true);
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
