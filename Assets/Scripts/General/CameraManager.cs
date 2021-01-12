using System;
using System.Collections.Generic;
using UnityEngine;

namespace SplitSpheres.General
{
    /// <summary>
    /// Manages Camera behavior
    /// </summary>
    public class CameraManager : MonoBehaviour
    {
        /// <summary>
        /// The speed of the Camera when it moves via SmoothMoveToPos()
        /// </summary>
        public float moveSpeed = 5f;
    
        
        /// <summary>
        /// Moves the camera smoothly vertically regarding a pos
        /// </summary>
        /// <param name="target">the Vector3 to target</param>
        public bool  SmoothMoveToPos(Vector3 target)
        {
            var position = transform.position;
            var verticalVector = new Vector3(position.x, target.y, position.x);

            transform.position = Vector3.MoveTowards(position, verticalVector, moveSpeed * Time.deltaTime);

            return Math.Abs(position.y - verticalVector.y) < .0001f;
        }

        /// <summary>
        /// Moves the camera smoothly vertically while rotating around a target
        /// </summary>
        /// <param name="target">the Vector3 to target</param>
        public bool SmoothMoveToPosAndRotate(Vector3 target)
        {
            
            var position = transform.position;
            var verticalVector = new Vector3(position.x, target.y, position.x);

            transform.position = Vector3.MoveTowards(position, verticalVector, moveSpeed * Time.deltaTime);
            transform.RotateAround(target, verticalVector, moveSpeed * Time.deltaTime);
            
            return Math.Abs(position.y - verticalVector.y) < .0001f;
        }

    }
}
