using System.Collections;
using SplitSpheres.Framework.GameEvents.Types;
using UnityEngine;

namespace SplitSpheres.Framework.GameEvents.Events
{
    [CreateAssetMenu(fileName = "NewVoidEvent", menuName = "GameEvents/VoidEvent")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());

        /// <summary>
        /// Raises the event with a specified delay
        /// </summary>
        /// <param name="_callerMonoBehaviour"> Object where the couroutine runs, usually the event raiser object</param>
        /// <param name="_delay">the delay of the event in seconds</param>
        public void Raise(MonoBehaviour _callerMonoBehaviour, float _delay)
        {
            _callerMonoBehaviour.StopCoroutine(RaiseRoutine(_delay));
            _callerMonoBehaviour.StartCoroutine(RaiseRoutine(_delay));
        }


        IEnumerator RaiseRoutine(float _delay)
        {
            yield return new WaitForSeconds(_delay);
            Raise();
        }
    }
}
