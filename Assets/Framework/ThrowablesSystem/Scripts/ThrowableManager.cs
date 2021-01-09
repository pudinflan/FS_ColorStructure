using System;
using Lean.Pool;
using UnityEngine;

namespace SplitSpheres.Framework.ThrowablesSystem.Scripts
{
    /// <summary>
    /// Manages a pool of Throwable Objects
    /// Displays 2 objects on World (_mainThrowable, _queuedThrowable) at their respective spots (mainThrowableDisplaySpot, queuedThrowableDisplaySpot)
    /// </summary>
    [RequireComponent(typeof(LeanGameObjectPool))]
    public class ThrowableManager : MonoBehaviour
    {
        /// <summary>
        /// The number of ThrowableObjects to load into ThrowablePool
        /// </summary>
        public int numberOfThrowables = 10; 
            
        /// <summary>
        /// The Object that will be Thrown
        /// </summary>
        public Throwable MainThrowable => _mainThrowable;

        /// <summary>
        /// Last object thrown
        /// </summary>
        public Throwable LastThrowable => _lastThrowable;

        /// <summary>
        /// A pool of ThrowableObjects
        /// </summary>
        protected LeanGameObjectPool ThrowablePool;

        /// <summary>
        /// Where on the World the MainThrowable will be displayed
        /// </summary>
        [SerializeField] private Transform mainThrowableDisplaySpot;

        /// <summary>
        /// Where on the World the QueuedThrowable will be displayed
        /// </summary>
        [SerializeField] private Transform queuedThrowableDisplaySpot;
        
        private Throwable _mainThrowable;
        private Throwable _lastThrowable;
        private Throwable _queuedThrowable;
        
        /// <summary>
        /// Removes an object from the DisplaySpots and Throws it towards a target
        /// Then updates the Throwables accordingly
        /// </summary>
        /// <param name="targetTransform">The target transform of Throwable</param>
        public virtual void ThrowThrowable(Transform targetTransform)
        {
            MainThrowable.Throw(targetTransform.position);
            UpdateThrowables();
        }

        /// <summary>
        /// Initializes ThrowablePool with the set numberOfThrowables
        /// </summary>
        protected virtual void Initialize()
        {
            ThrowablePool = GetComponent<LeanGameObjectPool>();
            
            //the number of Objects to load into the pool
            ThrowablePool.Preload = numberOfThrowables;
            //the max number of Objects that the pool uses without recycling the last
            ThrowablePool.Capacity = numberOfThrowables; 
            //Initializes the pool
            ThrowablePool.PreloadAll(); 

            //Loads throwables into DisplaySpots from the Pool
            LoadInitialThrowables(); 
        }
        
        /// <summary>
        /// Swaps Throwables, reSclales the new MainThrowable and saves their objects after being swapped, lastly queues a new Throwable
        /// </summary>
        protected virtual void UpdateThrowables()
        {
            _lastThrowable = _mainThrowable;
            _lastThrowable.transform.parent = null;

            _mainThrowable = _queuedThrowable;
            _mainThrowable.transform.parent = mainThrowableDisplaySpot;
            _mainThrowable.transform.position = mainThrowableDisplaySpot.position;
            _mainThrowable.transform.localScale = Vector3.one;

            _queuedThrowable = LoadThrowableIntoSpot(queuedThrowableDisplaySpot);
        }
        
        /// <summary>
        /// Loads and Displays the Initial MainThrowable and QueuedThrowable
        /// </summary>
        private void LoadInitialThrowables()
        {
            //Main Object to Throw
            _mainThrowable = LoadThrowableIntoSpot(mainThrowableDisplaySpot);
            //NextObject to Throw
            _queuedThrowable = LoadThrowableIntoSpot(queuedThrowableDisplaySpot);
        }

        /// <summary>
        /// Loads a Throwable into a DisplaySpot
        /// </summary>
        /// <param name="throwSpot">Where on world will the Throwable Spawn and be child of</param>
        /// <returns></returns>
        private Throwable LoadThrowableIntoSpot(Transform throwSpot)
        {
            return ThrowablePool.Spawn(throwSpot.position, Quaternion.identity, throwSpot)
                .GetComponent<Throwable>();
        }
    }
}