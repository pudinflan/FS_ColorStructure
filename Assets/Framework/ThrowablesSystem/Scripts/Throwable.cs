using System.Collections;
using UnityEngine;

namespace SplitSpheres.Framework.ThrowablesSystem.Scripts
{
    /// <summary>
    /// Throwable object that travels towards a target
    /// </summary>
    public abstract class Throwable : MonoBehaviour
    {
        /// <summary>
        /// The speed that Throwable moves to TargetPosition
        /// </summary>
        public float moveSpeed = 2f;
        
        /// <summary>
        /// The target Vector3 where the object will be thrown
        /// </summary>
        public Vector3 TargetPosition { get; private set; }

        public virtual bool ArrivedAtPosition => Vector3.Distance(transform.position, TargetPosition) <= 0.01f;
        
        /// <summary>
        /// Called when you want to throw the object
        /// </summary>
        /// <param name="targetPosition">The position to throw the object towards</param>
        public virtual void Throw(Vector3 targetPosition)
        {
            TargetPosition = targetPosition;
            StartCoroutine(ProcessThrow());
        }

        /// <summary>
        /// Routine that handles the travel of Throwable towards position
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator ProcessThrow()
        {
            while (!ArrivedAtPosition)
            {
                MoveThrowable();
                yield return new WaitForFixedUpdate();
            }
            
            OnArrival();
        }

        /// <summary>
        /// Called on Process Throw if the object is not at the target position
        /// </summary>
        protected virtual void MoveThrowable()
        {
            // Move our position a step closer to the target.
            var step =  moveSpeed * Time.fixedDeltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, step);
        }

        /// <summary>
        /// What will the object do when it arrives at a certain position
        /// </summary>
        public virtual void OnArrival()
        {
   
        }
    }
}