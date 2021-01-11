using System.Collections;
using SplitSpheres.Framework.ColorManagement;
using SplitSpheres.Framework.GameEvents.Events;
using UnityEngine;

namespace SplitSpheres.GameEvents
{
    [CreateAssetMenu(fileName = "NewCmColor32Event", menuName = "GameEvents/CmColor32Event")]
    public class CmColor32Event : BaseGameEvent<CmColor32>
    {
        public void Raise() => Raise(new CmColor32());
     
    }
}
