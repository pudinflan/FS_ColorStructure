using UnityEngine;

namespace SplitSpheres.Core.LevelGeneration
{
    /// <summary>
    /// Saves a Reference to a Level and PreparedLevel gameObject instance
    /// </summary>
    public class PreparedLevel
    {
        public Level Level;
        public GameObject PreparedLevelGOInstance;

        public PreparedLevel(Level level, GameObject preparedLevelGOInstance)
        {
            Level = level;
            PreparedLevelGOInstance = preparedLevelGOInstance;
        }
    }
}
