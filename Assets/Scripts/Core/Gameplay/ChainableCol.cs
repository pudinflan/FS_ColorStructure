using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplitSpheres.Core.Gameplay
{
    public class ChainableCol : MonoBehaviour
    {
        private HashSet<Cylinder> cylSet;
        private float searchRadius;
        
        private void Awake()
        {
            cylSet = new HashSet<Cylinder>();
        }

        public static void NewChainableCollision(Cylinder cylinder)
        {
            cylinder.gameObject.AddComponent<ChainableCol>().LookForCols(cylinder);
        }
        
        private void LookForCols(Cylinder cylinder)
        {
            searchRadius = transform.localScale.z * 1.1f;
            StartCoroutine(ProcessChainableCols(cylinder));
        }

        private IEnumerator ProcessChainableCols(Cylinder originalCyl)
        {
            while (CheckForCols(originalCyl))
            {
                yield return new WaitForSeconds(.1f);
            }

            foreach (var cylinder in cylSet)
            {
                cylinder.DestroySelf();
            }
            
            Destroy(originalCyl.GetComponent<ChainableCol>());
            originalCyl.DestroySelf();
        }

        private bool CheckForCols(Cylinder originalCyl)
        {
            //checks if there are same color cylinders in the vicinity
            var hitColliders = Physics.OverlapSphere(originalCyl.transform.position, searchRadius);

            foreach (var hitCollider in hitColliders)
            {
                var hitCyl = hitCollider.GetComponent<Cylinder>();

                if (hitCyl == null) continue;
                if (hitCyl.AssignedCmColor32.CompareColor(originalCyl.AssignedCmColor32.colorTag))
                {
                    if (!cylSet.Contains(hitCyl))
                    {
                        cylSet.Add(hitCyl);
                        CheckForCols(hitCyl);
                        return true;
                    }
                    
                }
            }

            return false;
        }
    }
}