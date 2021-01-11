using System;
using System.Collections.Generic;
using UnityEngine;

namespace SplitSpheres.Framework.ColorManagement
{
    /// <summary>
    /// Manages the color of object Material and its assignments
    /// </summary>
    public class CmColor32MaterialAssigner : MonoBehaviour
    {
        public CmColor32Material[] possibleMaterials;
        
        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void AssignMaterial()
        {
            
        }
    }

  
}
