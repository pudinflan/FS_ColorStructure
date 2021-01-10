using UnityEngine;

namespace SplitSpheres.Framework.ColorManagement
{
    [CreateAssetMenu(fileName = "CmColorCollection", menuName = "ColorManagement/CmColorCollection", order = 1)]
    public class CmColorCollection : ScriptableObject
    {
        public CmColor32[] colorCollectionArray;
    }
}