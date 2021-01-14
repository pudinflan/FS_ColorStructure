using System;
using Lean.Touch;
using SplitSpheres.Framework.ColorManagement;
using SplitSpheres.Framework.GameEvents.Events;
using UnityEngine;

namespace SplitSpheres.Core.Gameplay
{
    [RequireComponent(typeof(LeanSelectable))]
    public class Cylinder : MonoBehaviour
    {
        [Header("ActivationManagement")] 
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Material deactivatedMaterial;
        
        [Header("Color Management")]
        [SerializeField] private MeshRenderer mr;
        [SerializeField] private CmColor32 assignedCmColor32;
        
        [Header("Events")]
        [SerializeField] private Vector3Event vector3Event;

        public CmColor32 AssignedCmColor32 => assignedCmColor32;

        private bool _canBeSelected;
        
        private void Awake()
        {
            _canBeSelected = false;

            if (mr == null)
            {
                GetComponent<MeshRenderer>();
            }
        }

        public void AssignCmColor32(CmColor32 cmColor32ToAssign)
        {
            assignedCmColor32 = cmColor32ToAssign;
            mr.material = cmColor32ToAssign.cmColor32Material;
        }

        public void ActivateCylinder()
        {
            if (mr.material != null)
            {
                mr.material = assignedCmColor32.cmColor32Material;
            }

            _canBeSelected = true;
            rb.isKinematic = false;
        }

        public void DeactivateCylinder()
        {
            mr.material = deactivatedMaterial;
            
            _canBeSelected = false;
            rb.isKinematic = true;
        }
        
        /// <summary>
        /// Raises the Onselect event
        /// </summary>
        public void OnSelected()
        {
            if (!_canBeSelected)
            return;
            
            //Sends current position to Vector3EventListeners
            vector3Event.Raise(this.transform.position);
        }

        /// <summary>
        /// When called creates a Overlap Sphere to see if there are cylinders with the same color in the vicinity
        /// If so it chains to Other Cyls and does effects
        /// </summary>
        public void ProcessBallCollision()
        {
            ChainableCol.NewChainableCollision(this);
        }
        
        public void DestroySelf()
        {
            //TODO: IMPLEMENT HERE DESTRUCTION EVENT LIke SFX AND VIBRAT
            Destroy(this.gameObject);
        }
        

        //Joints
        /*private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Cylinder")) return;
    
            var joint = gameObject.AddComponent<FixedJoint>();
            joint.breakForce = 5000;
            joint.anchor = other.contacts[0].point; 
            joint.connectedBody = other.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>(); 
            joint.enableCollision = false;
        }*/
   
    }
}