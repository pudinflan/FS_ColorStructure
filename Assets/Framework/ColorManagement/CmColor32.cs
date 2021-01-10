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
        /// Compare if an instance of this CmColor32 is Equal to another.
        /// </summary>
        /// <param name="otherCmColor32"> The other color to Compare with ours</param>
        /// <returns>bool: comparision between colors</returns>
        public bool CompareColor(CmColor32 otherCmColor32)
        {
            return otherCmColor32.color32.r == color32.r &&
                   otherCmColor32.color32.g == color32.g &&
                   otherCmColor32.color32.b == color32.b &&
                   otherCmColor32.color32.a == color32.a;
        }
    }
}