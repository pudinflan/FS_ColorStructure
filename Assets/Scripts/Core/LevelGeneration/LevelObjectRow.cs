using System;
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

        /// <summary>
        /// Turns isKinematic = false, and Recolors to dark Grey for each cyl
        /// </summary>
        public void DeactivateCyls()
        {
            for (var index = rowOfCylinders.Length - 1; index >= 0; index--)
            {
                rowOfCylinders[index].DeactivateCylinder();
            }
        }
        
        /// <summary>
        /// Turns isKinematic = true, and Recolors to the assignedCmColor32
        /// </summary>
        public void ActivateCyls()
        {
            for (var index = rowOfCylinders.Length - 1; index >= 0; index--)
            {
                rowOfCylinders[index].ActivateCylinder();
            }
        }
    }
}