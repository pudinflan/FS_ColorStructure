using System.ComponentModel;
using UnityEngine;

namespace SplitSpheres.Framework.ColorManagement
{
    [System.Serializable]
    public struct CmColor32
    {
        /// <summary>
        /// My CmColor32 color
        /// </summary>
        public Color32 color32;

        /// <summary>
        /// The name of the Color used For Comparison
        /// </summary>
        public string colorTag;

        /// <summary>
        /// The material to assign to Other objects
        /// </summary>
        public Material cmColor32Material;
   
        /// <summary>
        /// Compares a color tag with this CmColor32 tag
        /// </summary>
        /// <param name="otherColorTag">other tag to compare to</param>
        /// <returns></returns>
        public bool CompareColor(string otherColorTag)
        {
            return colorTag == otherColorTag;
        }
    }
}