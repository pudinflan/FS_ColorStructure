using Lean.Touch;
using SplitSpheres.Framework.ColorManagement;
using SplitSpheres.Framework.GameEvents.Events;
using UnityEngine;

namespace SplitSpheres.Gameplay
{
    [RequireComponent(typeof(LeanSelectable))]
    public class Cylinder : MonoBehaviour
    {
        [SerializeField] private Vector3Event vector3Event;
        
        private CmColor32 _assignedCmColor32;
        private Collider[] _overlapResults = new Collider[10];
        
        public CmColor32 AssignedCmColor32
        {
            get => _assignedCmColor32;
            set => _assignedCmColor32 = value;
        }

        public void ProcessBallCollision()
        {
            //checks if there are same color cylinders in the vicinity
            var hitColliders = Physics.OverlapSphere(this.transform.position, transform.localScale.z);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Cylinder"))
                {
                    hitCollider.GetComponent<Cylinder>().ChainColorCollision(_assignedCmColor32);
                }
            }
        }

        public void ChainColorCollision(CmColor32 assignedCmColor32)
        {
            if (_assignedCmColor32.CompareColor(assignedCmColor32))
            {
                //TODO: CHANGE TO DEACTIVATE POOL AND SHOW VFX
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// Raises the Onselect event
        /// </summary>
        public void OnSelected()
        {
       
            //Sends current position to Vector3EventListeners
            vector3Event.Raise(this.transform.position);
        }
    
    
        //TODO: REMOVE THIS REGION WITH A COLOR ASSIGN MANAGER ON LEVEL CREATION
        #region DEBUG_TEMP

        [Header("DEBUG ONLY TEMP")] public CmColor32 TempoassignedColor;
        private void Start()
        {
            AssignedCmColor32 = TempoassignedColor;
        }
        
        
        #endregion

        
    }

   
}