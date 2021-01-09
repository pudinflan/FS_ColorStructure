using UnityEngine;

namespace SplitSpheres.Framework.SimpleFSM
{
    public class StateMachine 
    {
        private IState _currentlyRunningState;
        private IState _previousState;

        /// <summary>
        /// Changes the FSM to a New State
        /// </summary>
        /// <param name="newState">The state to change to</param>
        public void ChangeState(IState newState)
        {
            _currentlyRunningState?.Exit();
            _previousState = _currentlyRunningState;

            _currentlyRunningState = newState;
            _currentlyRunningState.Enter();
            
            Debug.Log("Changing State -> " + CurrentlyRunningStateName);
        }

        /// <summary>
        /// Call this in an Update or Couroutine to Execute the Update  State
        /// </summary>
        public void ExecuteStateUpdate() => _currentlyRunningState?.Execute();

        /// <summary>
        /// Call this to Change To the previous runned state
        /// </summary>
        public void SwitchToPreviousState()
        {
            _currentlyRunningState.Exit();
            _currentlyRunningState = _previousState;
            _currentlyRunningState.Enter();
        }

        public string CurrentlyRunningStateName => _currentlyRunningState.GetType().Name;

        public IState CurrentlyRunningState => _currentlyRunningState;
    }
}