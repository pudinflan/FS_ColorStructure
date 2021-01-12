using System.Collections.Generic;
using SplitSpheres.Framework.ColorManagement;
using UnityEngine;

namespace SplitSpheres.Core.LevelGeneration
{
    public class Level 
    {
        /// <summary>
        /// LevelObject Associated withThisLevel
        /// </summary>
        public LevelObject LevelObject;
        
        /// <summary>
        /// The number of balls to be used in the level
        /// </summary>
        public int numberOfBalls;
        
        /// <summary>
        /// The number of rows that start Inactive;
        /// </summary>
        public int numberOfInactiveRows;
        
        /// <summary>
        /// The colors that will be used in the level to later assign to Materials
        /// </summary>
        public CmColor32[] cmColor32S;

        public Level(CmColor32[] cmColor32S, int numberOfBalls,int numberOfInactiveRows, LevelObject levelObject)
        {
            this.cmColor32S = cmColor32S;
            this.numberOfBalls = numberOfBalls;
            this.LevelObject = levelObject;
            this.numberOfInactiveRows = numberOfInactiveRows;
        }
    }
}
