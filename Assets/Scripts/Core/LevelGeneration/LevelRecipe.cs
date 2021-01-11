using SplitSpheres.Core.Gameplay;
using SplitSpheres.Framework.ColorManagement;
using UnityEngine;

namespace SplitSpheres.Core.LevelGeneration
{
    /// <summary>
    ///Saves Reference to the level Prefab to be loaded, Its Cylinder objects and a CmColor32Collection to be used
    /// </summary>
    [CreateAssetMenu(fileName = "NewLevelRecipe", menuName = "LevelCreation/LevelRecipe")]
    public class LevelRecipe : ScriptableObject
    {
        /// <summary>
        /// The level object to be used that contains rows of cyllinders
        /// </summary>
        public LevelObject levelObject;

        /// <summary>
        /// The type of level (Circle, Square or Triangle)
        /// </summary>
        public LevelType levelType;

        /// <summary>
        /// The base number of balls on the level
        /// </summary>
        public int baseNumberOfBalls = 20;
        
        /// <summary>
        /// The possible colorCollection to be used in color generation and assignment
        /// </summary>
        public CmColorCollection[] colorCollection;
   
    }
}
