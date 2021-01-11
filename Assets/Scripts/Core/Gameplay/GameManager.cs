using System;
using System.Collections.Generic;
using SplitSpheres.Core.GameStates;
using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.SimpleFSM;
using SplitSpheres.Framework.Utils;
using UnityEngine;

namespace SplitSpheres.Core.Gameplay
{
    /// <summary>
    /// Manages GameStates and Level Flow
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// An array of level Recipes to use in Level Generation
        /// </summary>
        public LevelRecipe[] levelRecipes;

        /// <summary>
        /// The Ball ThrowableManager
        /// </summary>
        public BallThrowableManager ballThrowableManager;

        /// <summary>
        /// Handles the different GameStates, that control the game flow  -> LevelState, WinState and GameOverState
        /// </summary>
        private StateMachine gameStateMachine;

        /// <summary>
        /// Generated at the load of App
        /// </summary>
        private List<Level> generatedLevels;

        /// <summary>
        /// Can be modified as the Game levels progress to use as a difficulty modifier in GenerateLevels(), for example giving fewer balls to start with
        /// For the pourpouse of this prototype we will keep at 1
        /// </summary>
        private int levelReachedModifier = 1;

        private void Awake()
        {
            generatedLevels = new List<Level>();
        }

        private void Start()
        {
            GenerateLevels();

            //TODO; Write a routine that waits for levels to be generated
            InitializeGameState();
        }

        private void Update()
        {
            gameStateMachine?.ExecuteStateUpdate();
        }

        private void GenerateLevels()
        {
            foreach (var levelRecipe in levelRecipes)
            {
                var randomColorCmColorIndex = RandomInt.GenerateNumber(0, levelRecipe.colorCollection.Length);
                var randomCmColor32s = levelRecipe.colorCollection[randomColorCmColorIndex].colorCollectionArray;

                generatedLevels.Add(new Level(randomCmColor32s, levelRecipe.baseNumberOfBalls * levelReachedModifier, levelRecipe.levelObject));
            }
            
            
        }

        private void InitializeGameState()
        {
            //TODO: CHANGE THIS DEBUG ONLY
            var randomLevelIndex = RandomInt.GenerateNumber(0, generatedLevels.Count);
            //TODO: CHANGE THIS DEBUG ONLY
            
            
            gameStateMachine = new StateMachine();
            gameStateMachine.ChangeState(new LevelState(generatedLevels[randomLevelIndex]));
        }
    }
}