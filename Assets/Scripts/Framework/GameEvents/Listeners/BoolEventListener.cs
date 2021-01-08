using SplitSpheres.Framework.GameEvents.Events;
using SplitSpheres.Framework.GameEvents.UnityEvents;
using UnityEngine;

namespace SplitSpheres.Framework.GameEvents.Listeners
{
    public class BoolEventListener : BaseGameEventListener<bool, BoolEvent, UnityBoolEvent>
    {
        [Header("Bool state specific")]
        [SerializeField] UnityBoolEvent trueEventResponse;
        [SerializeField] UnityBoolEvent falseEventResponse;

        public override void OnEventRaised(bool item)
        {
            unityEventResponse?.Invoke(item);

            //print(GameEvent.name + "+" + item);

            if (item)
                trueEventResponse?.Invoke(item);
            else
                falseEventResponse?.Invoke(item);
        }
    }
}
