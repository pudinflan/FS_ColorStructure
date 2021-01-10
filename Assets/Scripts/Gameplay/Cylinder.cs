using System;
using Lean.Touch;
using SplitSpheres.Framework.ColorManagement;
using SplitSpheres.Framework.Utils;
using SplitSpheres.GameEvents;
using UnityEngine;
using Random = System.Random;

namespace SplitSpheres.Gameplay
{
    [RequireComponent(typeof(LeanSelectable))]
    public class Cylinder : MonoBehaviour
    {
        /// <summary>
        /// When a Cylinder is selected via its LeanSelectable Component it sends this CmColo32 to any listener via the OnSelected() Method,
        /// so that listener can do something with the cylinder color like comparison, assignment, etc.
        /// </summary>
        [SerializeField] private CmColor32Event colorEvent;

        private CmColor32 _assignedCmColor32;
        

        /// <summary>
        /// Does something when player clicks the cyllinder
        /// </summary>
        public void OnSelected()
        {
            //raises the current assigned color
            colorEvent.Raise(_assignedCmColor32);
            Debug.Log("Raising Assigned Color: " + _assignedCmColor32.color32);
        }


        //TODO: REMOVE THIS REGION WITH A COLOR ASSIGN MANAGER ON LEVEL CREATION
        #region DEBUG_TEMP

        [Header("DEBUG ONLY TEMP")] public CmColorCollection tempColorCollection;
        private void Start()
        {
            _assignedCmColor32 = GetRandomActiveColor();
        }
        
        private  CmColor32 GetRandomActiveColor()
        {
            var randomNumber = RandomInt.GenerateNumber(0, tempColorCollection.colorCollectionArray.Length);

            return tempColorCollection.colorCollectionArray[randomNumber];
        }

        #endregion
    }

   
}