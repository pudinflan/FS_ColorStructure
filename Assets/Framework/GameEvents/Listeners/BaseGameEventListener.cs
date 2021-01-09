using SplitSpheres.Framework.GameEvents.Events;
using UnityEngine;
using UnityEngine.Events;

namespace SplitSpheres.Framework.GameEvents.Listeners
{
    /// <summary>
    /// Base Listerner class that get inerithed
    /// </summary>
    /// <typeparam name="T"> Type of Listener</typeparam>
    /// <typeparam name="E"> Event that should Listen too</typeparam>
    /// <typeparam name="UER"> Unity Event Response</typeparam>
    public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour, IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] private E gameEvent;
        public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

        [SerializeField] protected UER unityEventResponse;
        
        [Tooltip("Note taking. This won't be in build.")]
        [TextArea(3, 8)]
        [SerializeField] string notes;

        private void OnEnable()
        {
            if (gameEvent == null) { return; }

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent == null) { return; }

            GameEvent.UnregisterListener(this);
        }

        public virtual void OnEventRaised(T item)
        {
            if (unityEventResponse != null)
            {
                unityEventResponse.Invoke(item);
            }
        }
    }
}
