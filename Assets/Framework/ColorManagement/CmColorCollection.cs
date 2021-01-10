using UnityEngine;

namespace SplitSpheres.Framework.ColorManagement
{
    /// <summary>
    /// A ScriptableObject with a Collection fo CmColor32 Objects
    /// </summary>
    [CreateAssetMenu(fileName = "CmColorCollection", menuName = "ColorManagement/CmColorCollection", order = 1)]
    public class CmColorCollection : ScriptableObject
    {
        public CmColor32[] colorCollectionArray;
    }
}