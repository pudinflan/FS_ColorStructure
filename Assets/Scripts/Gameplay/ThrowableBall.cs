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

      
        public override void OnArrival()
        {
            onArrivalEvent.Raise();
          
        }

        public void OnSpawn()
        {
           
        }

        public void OnDespawn()
        {
         
        }
    }
}
