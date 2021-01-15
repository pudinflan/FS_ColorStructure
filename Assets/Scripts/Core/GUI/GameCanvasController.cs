using System;
using SplitSpheres.Framework.GUI.Scripts;
using UnityEngine;

namespace SplitSpheres.Core.GUI
{
    /// <summary>
    /// Controls and saves the various ui elements used in a levelstate
    /// </summary>
    public class GameCanvasController : MonoBehaviour
    {
        public ProgressBar progressBar;
        public BallThrowablePanel ballThrowablePanel;
        
        private CanvasGroup canvasGroup;
        
        private void Awake()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
            
            TurnOnGameCanvas(false);
        }

        public void TurnOnGameCanvas(bool turnOn)
        {
            canvasGroup.alpha = turnOn ? 1f : 0f;
        }
        
    }
}
