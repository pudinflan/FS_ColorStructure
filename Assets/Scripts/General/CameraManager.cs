using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using SplitSpheres.Core.LevelGeneration;
using UnityEditor;
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
        /// The Y offset to apply to vertical movement when moving to a target
        /// </summary>
        public float yOffset = -2.5f;

        /// <summary>
        /// The number of the line where the camera will stop descending
        /// </summary>
        public int stopMoveAtLine = 8;
        
        /// <summary>
        /// The Speed of the camera dragging
        /// </summary>
        public float camDragSpeed = 2f;

        private Vector2 dragStart;
        private Camera cam;
        private int lastCheckRowIndex = 0;

        private void Start()
        {
            cam = GetComponent<Camera>();
        }

        void OnEnable()
        {
            Lean.Touch.LeanTouch.OnFingerDown += HandleFingerDown;
            Lean.Touch.LeanTouch.OnFingerUpdate += HandleFinger;
            LevelObjectRow.onRowEmpty += MoveToRow;
        }

        void OnDisable()
        {
            Lean.Touch.LeanTouch.OnFingerDown -= HandleFingerDown;
            Lean.Touch.LeanTouch.OnFingerUpdate -= HandleFinger;
            LevelObjectRow.onRowEmpty -= MoveToRow;
        }

        private void MoveToRow(int rowindex, Vector3 pos)
        {

            Debug.Log("rowindex " + rowindex + "lastCheckRowIndex: " + lastCheckRowIndex);
            
            if ( lastCheckRowIndex >= rowindex )
            {
                if (rowindex > stopMoveAtLine)
                {
                    transform.position = new Vector3(transform.position.x, pos.y + yOffset *2 , transform.position.z);
                }
                lastCheckRowIndex = rowindex;
                Handheld.Vibrate();
            }
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
            var position = VerticalMovement(target, out var verticalVector);

            return Math.Abs(position.y - verticalVector.y) < .0001f;
        }
        
        /// <summary>
        /// Moves the camera smoothly vertically while rotating around a target
        /// </summary>
        /// <param name="target">the Vector3 to target</param>
        /// <param name="accel">OPTIONAL: send a speed Modifier</param>
        public bool SmoothMoveToPosAndRotate(Vector3 target,float accel = 1f)
        {
            var position = VerticalMovement(target, out var verticalVector);
            RotateAroundTarget(target, moveSpeed * Time.deltaTime * 10f * accel);
            
            return Math.Abs(transform.position.y - verticalVector.y) < .0001f;
        }
        
        //TODO: Probably PUT THIS ON THE START OF THE STATE_CAMERA_GAME when FSM is implemented
        public void InitializeCamLevelState()
        {
            lastCheckRowIndex = 100;
        }
        
        private Vector3 VerticalMovement(Vector3 target, out Vector3 verticalVector)
        {
            var position = transform.position;
            verticalVector = new Vector3(position.x, target.y + yOffset, position.z);

            transform.position = Vector3.MoveTowards(position, verticalVector, moveSpeed * Time.deltaTime);
            return position;
        }

        private void RotateAroundTarget(Vector3 target, float angle)
        {
            transform.RotateAround(target, Vector3.up, angle);
        }

    }
}
