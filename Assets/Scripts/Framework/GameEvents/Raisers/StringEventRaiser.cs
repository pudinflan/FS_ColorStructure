using SplitSpheres.Framework.GameEvents.Events;
using UnityEngine;

namespace SplitSpheres.Framework.GameEvents.Raisers
{
    public class StringEventRaiser : EventRaiser
    {
        // Game events list
        [SerializeField] StringEvent[] gameEvent;

        public virtual void RaiseEvent(string eventAttribute)
        {
            // Raise an event
            StartCoroutine(RaisingEvent(gameEvent, eventAttribute));
        }

    }
}
