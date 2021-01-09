using SplitSpheres.Framework.GameEvents.Events;
using UnityEngine;

namespace SplitSpheres.Framework.GameEvents.Raisers
{
    public class IntEventRaiser : EventRaiser
    {
        // Game events list
        [SerializeField] IntEvent[] gameEvent;

        public virtual void RaiseEvent(int eventAttribute)
        {
            // Raise an event
            StartCoroutine(RaisingEvent(gameEvent, eventAttribute));
        }
    }
}
