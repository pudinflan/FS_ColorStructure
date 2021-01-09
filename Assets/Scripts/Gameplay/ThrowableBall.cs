using System;
using SplitSpheres.Framework.GameEvents.Events;
using SplitSpheres.Framework.ThrowablesSystem.Scripts;
using UnityEngine;

namespace SplitSpheres.Gameplay
{
    public class ThrowableBall : Throwable
    {
        
        //TODO: CRIAR UM COLOR EVENT QUE ENVIA A COR QUE é QUANDO é ENVIADo, A BOLA VERIFICA A COR E REPONDE SE FOR IGUAL
        [SerializeField] private VoidEvent onArrivalEvent;
        
        public override void OnArrival()
        {
            onArrivalEvent.Raise();
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
            if (other.gameObject.CompareTag("Cylinder"))
            {
                Destroy(other.gameObject);
            }
        }

    }
}
