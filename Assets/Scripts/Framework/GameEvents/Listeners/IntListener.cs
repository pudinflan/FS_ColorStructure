using SplitSpheres.Framework.GameEvents.Events;
using SplitSpheres.Framework.GameEvents.UnityEvents;
using UnityEngine;

namespace SplitSpheres.Framework.GameEvents.Listeners
{
    public class IntListener : BaseGameEventListener<int, IntEvent, UnityIntEvent>
    {
        [Header("Integer specific")]
        [SerializeField] bool onlySpecifiedItem;
        [SerializeField] int specificInteger;

        public override void OnEventRaised(int item)
        {
            if (unityEventResponse != null)
            {
                // If onlySpecifiedItem is True,
                // check if item is the same as specificInteger.
                // If not, leave method
                if (onlySpecifiedItem)
                    if (specificInteger != item)
                        return;

                unityEventResponse.Invoke(item);
            }
        }
    }


}
