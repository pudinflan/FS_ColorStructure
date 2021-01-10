using System;
using Lean.Pool;
using SplitSpheres.Framework.GameEvents.Events;
using SplitSpheres.Framework.ThrowablesSystem.Scripts;
using UnityEngine;

namespace SplitSpheres.Gameplay
{
    public class ThrowableBall : Throwable, IPoolable
    {
        
        //TODO: CRIAR UM COLOR EVENT QUE ENVIA A COR QUE é QUANDO é ENVIADo, A BOLA VERIFICA A COR E REPONDE SE FOR IGUAL
        [SerializeField] private VoidEvent onArrivalEvent;

        //Has the ball collided with a cyllinder?
        private bool _hasCollidedWithCyl = false;

        public override void OnArrival()
        {
            onArrivalEvent.Raise();
            _hasCollidedWithCyl = true;
        }
     
        
        //DEBUG
        
        private void Awake()
        {
            this.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();
        }

        
        //TODO: DEBUG DEBUGH DEBUG DEBUG
        
        //METER ISTO NO EVENTO ONARRIVE EM VEZ DE SER AQUI
        private void OnCollisionEnter(Collision other)
        {
            if(_hasCollidedWithCyl) return;
            if (!other.gameObject.CompareTag("Cylinder")) return;
            

            Destroy(other.gameObject);
        }

        public void OnSpawn()
        {
            //TODO: Generate new CMColor32 or Get a Active CMColor32
        }

        public void OnDespawn()
        {
            //TODO: Turn Ob Collidable
            _hasCollidedWithCyl = false;
        }
    }
}
