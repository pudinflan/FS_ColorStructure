using SplitSpheres.Framework.GameEvents.Events;
using UnityEngine;

namespace SplitSpheres.Framework.GameEvents.Raisers
{
    public class BoolEventRaiser : EventRaiser
    {
        // Game events list
        [SerializeField] BoolEvent[] gameEvent;

        public virtual void RaiseEvent(bool eventAttribute)
        {
            // Raise an event
            StartCoroutine(RaisingEvent(gameEvent, eventAttribute));
        }
    }
}
