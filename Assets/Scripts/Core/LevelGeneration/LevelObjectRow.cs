using System;
using System.Collections.Generic;
using System.Linq;
using SplitSpheres.Core.Gameplay;
using SplitSpheres.Core.GameStates;
using UnityEngine;

namespace SplitSpheres.Core.LevelGeneration
{
    /// <summary>
    ///Cylinders in a certain row of a LevelObject
    /// </summary>
    public class LevelObjectRow : MonoBehaviour
    {
        public List<Cylinder> rowOfCylinders;
        private  Collider fallCheckCol;

        public delegate void RowEmpty(int rowIndex, Vector3 rowPosition);
        public static event RowEmpty ONRowEmpty;
        
        public delegate void CylDropped();
        public static event CylDropped ONCylDropped;
        
        private bool alreadyActivated = false;
        
        public int RowIndex { get; set; }

        public bool AlreadyActivated
        {
            get => alreadyActivated;
            set => alreadyActivated = value;
        }
        
        
        private void Awake()
        {
            fallCheckCol = GetComponent<Collider>();
     
 
        }

        /// <summary>
        /// Turns isKinematic = false, and Recolors to dark Grey for each cyl
        /// </summary>
        public void DeactivateCyls()
        {     
        
            for (var index = rowOfCylinders.Count - 1; index >= 0; index--)
            {
                rowOfCylinders[index]?.DeactivateCylinder();
            }
       
            fallCheckCol.enabled = false;
        }
        
        /// <summary>
        /// Turns isKinematic = true, and Recolors to the assignedCmColor32
        /// </summary>
        public void ActivateCyls()
        {
            
            for (var index = rowOfCylinders.Count - 1; index >= 0; index--)
            {
                if (rowOfCylinders[index] != null)
                {
                    rowOfCylinders[index].ActivateCylinder();
                }
                
            }

            alreadyActivated = true;
            fallCheckCol.enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Cylinder")) return;
            
            var checkingCyl = other.GetComponent<Cylinder>();
            //if the cyl is not already checked keep going
            if (!rowOfCylinders.Contains(checkingCyl)) return; 

            ONCylDropped?.Invoke();
            rowOfCylinders.Remove(checkingCyl);
            
            rowOfCylinders = rowOfCylinders.Where(x => x != null).ToList();

            if (rowOfCylinders.Any()) return;
            ONRowEmpty?.Invoke(RowIndex, this.transform.position);
        }

      
    }
}