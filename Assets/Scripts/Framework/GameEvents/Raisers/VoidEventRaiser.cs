using SplitSpheres.Framework.GameEvents.Events;
using SplitSpheres.Framework.GameEvents.Types;
using UnityEngine;

namespace SplitSpheres.Framework.GameEvents.Raisers
{
    public class VoidEventRaiser : EventRaiser
    {
        [SerializeField] VoidEvent[] gameEvent;

        public virtual void RaiseEvent()
        {
            // Raise an event
            StartCoroutine(RaisingEvent(gameEvent, new Void()));
        }
    }
}
