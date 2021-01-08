namespace SplitSpheres.Framework.SimpleFSM
{
    public interface IState
    {
        //What the state should do when its created  and inserted in the StateMachine
        void Enter();

        //What the state repeatedly does while is on the StateMachine
        void Execute();
        
        //What the sate does when its Removed from the StateMachine
        void Exit();

        void OnDestroy();
    }
}