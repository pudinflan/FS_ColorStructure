using System;
using System.Collections;
using Lean.Touch;
using SplitSpheres.Framework.ColorManagement;
using SplitSpheres.Framework.GameEvents.Events;
using SplitSpheres.Framework.Utils;
using UnityEngine;

namespace SplitSpheres.Core.Gameplay
{
    [RequireComponent(typeof(LeanSelectable))]
    public class Cylinder : MonoBehaviour
    {
        [Header("ActivationManagement")] [SerializeField]
        private Rigidbody rb;

        [SerializeField] private Material deactivatedMaterial;

        [Header("Color Management")] [SerializeField]
        private MeshRenderer mr;

        [SerializeField] private CmColor32 assignedCmColor32;

        [Header("Events")] [SerializeField] private Vector3Event vector3Event;

        [Header("Special Cyl Stuff")] [SerializeField]
        private CmColorCollection specialCylColorCollection;

        [SerializeField] private float colorChangeSpeedRate = 3f;

        private bool imSpecial;

        public CmColor32 AssignedCmColor32 => assignedCmColor32;

        public bool CanBeSelected { get; set; }

  

        public delegate void CylDestroyed();

        public static event CylDestroyed onCylDestroyed;

        private void Awake()
        {
            CanBeSelected = false;
           

            if (mr == null)
            {
                GetComponent<MeshRenderer>();
            }
            
     
        }

        private void Start()
        {
            imSpecial = RandomInt.GenerateNumber(0, 20) > 15;

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

            CanBeSelected = true;
            rb.isKinematic = false;
            
            if (!imSpecial) return;
            StartCoroutine(HandleSpecialCyl());

        }


        public void DeactivateCylinder()
        {
            mr.material = deactivatedMaterial;

            CanBeSelected = false;
            rb.isKinematic = true;
        }

        /// <summary>
        /// Raises the Onselect event
        /// </summary>
        public void OnSelected()
        {
            if (!CanBeSelected)
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
            onCylDestroyed?.Invoke();
            //TODO: IMPLEMENT HERE DESTRUCTION EVENT LIke SFX AND VIBRAT
            Destroy(this.gameObject);
        }


        private IEnumerator HandleSpecialCyl()
        {
            while (true)
            {
                AssignCmColor32(specialCylColorCollection.colorCollectionArray[
                    RandomInt.GenerateNumber(0, specialCylColorCollection.colorCollectionArray.Length)]);
                yield return new WaitForSeconds(colorChangeSpeedRate);
            }
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