using UnityEngine;

namespace SplitSpheres.Framework.GameEvents.Events
{
    [CreateAssetMenu(fileName = "NewStringEvent", menuName = "GameEvents/StringEvent")]
    public class StringEvent : BaseGameEvent<string>
    {
    }
}
