using System;
using System.Collections.Generic;
using System.Linq;
using SplitSpheres.Core.Gameplay;
using UnityEngine;

namespace SplitSpheres.Core.LevelGeneration
{
    /// <summary>
    ///Cylinders in a certain row of a LevelObject
    /// </summary>
    public class LevelObjectRow : MonoBehaviour
    {
        public Cylinder[] rowOfCylinders;

        private  Collider fallCheckCol;
        private List<Cylinder> checkedCylinders;

        public delegate void RowEmpty(int rowIndex, Vector3 rowPosition);
        public static event RowEmpty onRowEmpty;
        
        
        private void Awake()
        {
            fallCheckCol = GetComponent<Collider>();
            checkedCylinders = new List<Cylinder>(rowOfCylinders);
            SpawnedIndex = rowOfCylinders.Length - 1;
 
        }

        /// <summary>
        /// The position of the row in game
        /// </summary>
        public int SpawnedIndex { get; private set; } = 0;

        public int RowIndex { get; set; }

        /// <summary>
        /// Turns isKinematic = false, and Recolors to dark Grey for each cyl
        /// </summary>
        public void DeactivateCyls()
        {     
        
            for (var index = rowOfCylinders.Length - 1; index >= 0; index--)
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
            
            for (var index = rowOfCylinders.Length - 1; index >= 0; index--)
            {
                if (rowOfCylinders[index] != null)
                {
                    
                rowOfCylinders[index].ActivateCylinder();
                }
                
            }
            
            fallCheckCol.enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Cylinder")) return;
            
            var checkingCyl = other.GetComponent<Cylinder>();
            //if the cyl is not already checked keep going
            if (!checkedCylinders.Contains(checkingCyl)) return; 

            checkedCylinders.Remove(checkingCyl);
            
            SpawnedIndex--;
          
            if (SpawnedIndex <= 0)
            {
                
                if (onRowEmpty != null) 
                    onRowEmpty(RowIndex, this.transform.position);
            }
        }

      
    }
}