using System;
using System.Collections.Generic;
using Lean.Touch;
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
        /// The Speed of the camera dragging
        /// </summary>
        public float camDragSpeed = 2f;

        private Vector2 dragStart;
        private Camera cam;

        private void Start()
        {
            cam = GetComponent<Camera>();
        }

        void OnEnable()
        {
            Lean.Touch.LeanTouch.OnFingerDown += HandleFingerDown;
            Lean.Touch.LeanTouch.OnFingerUpdate += HandleFinger;
        }

        void OnDisable()
        {
            Lean.Touch.LeanTouch.OnFingerDown -= HandleFingerDown;
            Lean.Touch.LeanTouch.OnFingerUpdate -= HandleFinger;
        }

      
        
        void HandleFingerDown(Lean.Touch.LeanFinger finger)
        {
            dragStart = finger.ScreenPosition;
        }
        
        void HandleFinger(Lean.Touch.LeanFinger finger)
        {
            Vector3 drag = cam.ScreenToViewportPoint(finger.ScreenPosition - dragStart);

            RotateAroundTarget(Vector3.zero,drag.x);
        }
        
        
        /// <summary>
        /// Moves the camera smoothly vertically regarding a pos
        /// </summary>
        /// <param name="target">the Vector3 to target</param>
        public bool  SmoothMoveToPos(Vector3 target)
        {
            var position = transform.position;
            var verticalVector = new Vector3(position.x, target.y, position.z);

            transform.position = Vector3.MoveTowards(position, verticalVector, moveSpeed * Time.deltaTime);

            return Math.Abs(position.y - verticalVector.y) < .0001f;
        }

        /// <summary>
        /// Moves the camera smoothly vertically while rotating around a target
        /// </summary>
        /// <param name="target">the Vector3 to target</param>
        /// <param name="accel">OPTIONAL: send a speed Modifier</param>
        public bool SmoothMoveToPosAndRotate(Vector3 target,float accel = 1f)
        {
            var verticalVector = new Vector3(transform.position.x, target.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, verticalVector, moveSpeed * Time.deltaTime );
          
            RotateAroundTarget(target, moveSpeed * Time.deltaTime * 10f * accel);
            
            return Math.Abs(transform.position.y - verticalVector.y) < .0001f;
        }

        private void RotateAroundTarget(Vector3 target, float angle)
        {
            transform.RotateAround(target, Vector3.up, angle);
        }

    }
}
