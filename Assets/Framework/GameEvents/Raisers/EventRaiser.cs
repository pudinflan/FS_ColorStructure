using System.Collections;
using SplitSpheres.Framework.GameEvents.Events;
using UnityEngine;

namespace SplitSpheres.Framework.GameEvents.Raisers
{
    public class EventRaiser : MonoBehaviour
    {
        // This is changed before calling the event of this index
        protected int currentGameEventIndex;

        // Change game event index, so that we can call the right event
        public virtual void ChangeGameEventIndex(int newIndex)
        {
            currentGameEventIndex = newIndex;
        }

        // Raise event after its delay time
        protected virtual IEnumerator RaisingEvent<T>(BaseGameEvent<T>[] gameEvent, T eventArg)
        {
            // Save current event index, so that
            // it doesn't get changed internally

            int eventIndex = currentGameEventIndex;

            yield return new WaitForSeconds(gameEvent[eventIndex].EventDelay);

            gameEvent[eventIndex].Raise(eventArg);
        }
    }
}
