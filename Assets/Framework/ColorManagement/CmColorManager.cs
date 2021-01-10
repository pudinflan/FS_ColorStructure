using UnityEngine;
using System;

namespace SplitSpheres.Framework.ColorManagement
{
    /// <summary>
    /// Manages Collection of CmColors that are active on a Scene
    /// Has various methods to keep track and manage Objects with CmColor32
    /// </summary>
    public class CmColorManager : MonoBehaviour
    {
        public CmColorCollection activeCmC32Coll;

        public virtual CmColor32 GetRandomActiveColor()
        {
            var random = new System.Random();
            var randomNumber = random.Next(0, activeCmC32Coll.colorCollectionArray.Length);

            return activeCmC32Coll.colorCollectionArray[randomNumber];
        }
    }
}