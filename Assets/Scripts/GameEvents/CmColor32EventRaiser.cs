using SplitSpheres.Framework.ColorManagement;
using SplitSpheres.Framework.GameEvents.Raisers;
using UnityEngine;

namespace SplitSpheres.GameEvents
{
    public sealed class CmColor32EventRaiser : EventRaiser
    {
        [SerializeField] CmColor32Event[] gameEvent;

        public void RaiseEvent(CmColor32 eventAttribute)
        {
            // Raise an event
            StartCoroutine(RaisingEvent(gameEvent, eventAttribute));
        }
    }
 
}
