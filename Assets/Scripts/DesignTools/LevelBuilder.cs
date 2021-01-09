using System;
using UnityEngine;

namespace SplitSpheres.DesignTools
{
    /// <summary>
    /// Tool For building Levels Prefabs
    /// </summary>
    public class LevelBuilder: MonoBehaviour
    {
        public GameObject destructiblePrefab;
        public int baseRadius = 10;
        public int divisions = 10;
        
        private MeshRenderer _mr;

        private void Awake()
        {
            _mr = GetComponent<MeshRenderer>();
        }
        
        private void BuildRow()
        {
            
        }

        //Divides Play area into equal parts
        private void DividePlayArea()
        {
            //create a temp circle with the radius of the base
            /*var bounds = _mr.bounds;
            var center = bounds.center;
            var radius = bounds.extents.magnitude;*/

            var center = Vector3.zero;
            var circlePerimeter = 2 * baseRadius * Mathf.PI;
            var startingSpawnPosition = new Vector3(center.x,center.y, center.z + baseRadius);

            for (int i = 0; i < divisions; i++)
            {
               // var newPos 
               // Instantiate(destructiblePrefab, newPos, Quaternion.identity);
            }
        }
        
    }
}