using SplitSpheres.Framework.GameEvents.Events;
using UnityEngine;

namespace SplitSpheres.Framework.GameEvents.Raisers
{
    public sealed class Vector3EventRaiser : EventRaiser
    {
        // Game events list
        [SerializeField] BaseGameEvent<Vector3>[] gameEvent;

        public void RaiseEvent(Vector3 eventAttribute)
        {
            // Raise an event
            StartCoroutine(RaisingEvent(gameEvent, eventAttribute));
        }
    }
}
