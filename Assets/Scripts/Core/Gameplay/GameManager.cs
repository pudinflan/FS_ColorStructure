
using SplitSpheres.Core.GameStates;
using SplitSpheres.Core.GUI;
using SplitSpheres.Core.LevelGeneration;
using SplitSpheres.Framework.GUI.Scripts;
using SplitSpheres.Framework.SimpleFSM;
using SplitSpheres.Framework.Utils;
using SplitSpheres.General;
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
        /// The CameraManager attached to the main Camera
        /// </summary>
        public CameraManager cameraManager;
        
        /// <summary>
        /// The Ball ThrowableManager
        /// </summary>
        public BallThrowableManager ballThrowableManager;

        /// <summary>
        /// The Game Canvas Controller
        /// </summary>
        public GameCanvasController gameCanvasController;
        
        /// <summary>
        /// Handles the different GameStates, that control the game flow  -> LevelState, WinState and GameOverState
        /// </summary>
        public StateMachine GameStateMachine;

        /// <summary>
        /// Generated at the load of App
        /// </summary>
        private Level generatedLevel;

        /// <summary>
        /// A reference to to the Generated LevelObject 
        /// </summary>
        private GameObject generatedLevelsGO;

        /// <summary>
        /// Can be modified as the Game levels progress to use as a difficulty modifier in GenerateLevels(), for example giving fewer balls to start with
        /// For the pourpouse of this prototype we will keep at 1
        /// </summary>
        private int levelReachedModifier = 1;

        /// <summary>
        /// A reference to the currentLevel State that is running
        /// </summary>
        private LevelState currentLevelstate;

        private void Start()
        {
            //Generates a Level and saves it TODO: Generate various levels in a pool and just activate them, instead of generating every time a new gamestate is created

            if (ballThrowableManager == null)
            {
                ballThrowableManager = GameObject.FindObjectOfType<BallThrowableManager>();
            }

            if (cameraManager == null)
            {
                if (Camera.main is { }) cameraManager = Camera.main.GetComponent<CameraManager>();
            }

            PrepareNewLevel();
        }

        private void Update()
        {
            GameStateMachine?.ExecuteStateUpdate();
        }

        public void StartNewLevel()
        {
            currentLevelstate.ActivateLevel();
        }
        
        private void PrepareNewLevel()
        {
            GameStateMachine = new StateMachine();
            currentLevelstate = new LevelState(PrepareLevel(), this);
            GameStateMachine.ChangeState(currentLevelstate);
        }

        private PreparedLevel PrepareLevel()
        {
            var randomLevelIndex = RandomInt.GenerateNumber(0, levelRecipes.Length);
            var levelRecipe = levelRecipes[randomLevelIndex];
            var randomColorCmColorIndex = RandomInt.GenerateNumber(0, levelRecipe.colorCollection.Length);
            var randomCmColor32S = levelRecipe.colorCollection[randomColorCmColorIndex].colorCollectionArray;

            generatedLevel = new Level(randomCmColor32S, levelRecipe.baseNumberOfBalls * levelReachedModifier, levelRecipe.baseNumberOfInactiveRows,
                levelRecipe.levelObject);

            SaveAndColorGeneratedLevel(levelRecipe);

            return new PreparedLevel(generatedLevel, generatedLevelsGO);
        }

        private void SaveAndColorGeneratedLevel(LevelRecipe levelRecipe)
        {
            foreach (var levelObjectRow in generatedLevel.LevelObject.cylRows)
            {
                foreach (var cyl in levelObjectRow.rowOfCylinders)
                {
                    cyl.AssignCmColor32(
                        generatedLevel.cmColor32S[RandomInt.GenerateNumber(0, generatedLevel.cmColor32S.Length)]);
                }
            }

            generatedLevelsGO = Instantiate(levelRecipe.levelObject.gameObject);
            generatedLevelsGO.SetActive(false);
        }
    }
}