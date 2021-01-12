using System;
using System.Collections.Generic;
using Lean.Pool;
using SplitSpheres.Core.GameStates;
using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.SimpleFSM;
using SplitSpheres.Framework.Utils;
using UnityEngine;
using Random = System.Random;

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
        /// A stack of Generated LevelObjects 
        /// </summary>
        private List<GameObject> generatedLevelsGOs;


        /// <summary>
        /// Can be modified as the Game levels progress to use as a difficulty modifier in GenerateLevels(), for example giving fewer balls to start with
        /// For the pourpouse of this prototype we will keep at 1
        /// </summary>
        private int levelReachedModifier = 1;

        /// <summary>
        /// The index of the current Level that loaded on the generatedLevels and generatedLevelsGOs
        /// </summary>
        private int currentPreparedLevelIndex = 0;

        private void Awake()
        {
            generatedLevels = new List<Level>();
            generatedLevelsGOs = new List<GameObject>();
            currentPreparedLevelIndex = 0;
        }

        private void Start()
        {
            //Generates a Level list and saves it, also instantiates and saves reference to the generated level prefabs
            GenerateLevels();
            InitializeGameState();
        }

        private void Update()
        {
            gameStateMachine?.ExecuteStateUpdate();
        }

        private void GenerateLevels()
        {
            for (var i = 0; i < levelRecipes.Length; i++)
            {
                var levelRecipe = levelRecipes[i];
                var randomColorCmColorIndex = RandomInt.GenerateNumber(0, levelRecipe.colorCollection.Length);
                var randomCmColor32s = levelRecipe.colorCollection[randomColorCmColorIndex].colorCollectionArray;

                generatedLevels.Add(new Level(randomCmColor32s, levelRecipe.baseNumberOfBalls * levelReachedModifier,
                    levelRecipe.levelObject));

                SaveAndColorGeneratedLevels(levelRecipe, i);
            }
        }

        private void SaveAndColorGeneratedLevels(LevelRecipe levelRecipe, int i)
        {
            foreach (var generatedLevel in generatedLevels)
            {
                foreach (var levelObjectRow in generatedLevel.LevelObject.cylRows)
                {
                    foreach (var cyl in levelObjectRow.rowOfCylinders)
                    {
                        cyl.AssignCmColor32(
                            generatedLevel.cmColor32S[RandomInt.GenerateNumber(0, generatedLevel.cmColor32S.Length)]);
                    }
                }
            }

            generatedLevelsGOs.Add(levelRecipe.levelObject.gameObject);
            Instantiate(generatedLevelsGOs[i]);

            generatedLevelsGOs[i].SetActive(false);
        }

        private void InitializeGameState()
        {
            //TODO: CHANGE THIS DEBUG ONLY
            var randomLevelIndex = RandomInt.GenerateNumber(0, generatedLevels.Count);
            //TODO: CHANGE THIS DEBUG ONLY


            gameStateMachine = new StateMachine();
            gameStateMachine.ChangeState(new LevelState(PrepareLevel(), ballThrowableManager));
        }

        private PreparedLevel PrepareLevel()
        {
            //TODO: too slow Change This
            if (generatedLevels.Count == 0)
            {
                GenerateLevels();
            }

            var level = generatedLevels[currentPreparedLevelIndex];
            var levelPrefab = generatedLevelsGOs[currentPreparedLevelIndex];

            generatedLevels.RemoveAt(currentPreparedLevelIndex);
            generatedLevelsGOs.RemoveAt(currentPreparedLevelIndex);

            currentPreparedLevelIndex++;
            return new PreparedLevel(level, levelPrefab);
        }
    }
}