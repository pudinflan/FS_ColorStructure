using System;
using TMPro;
using UnityEngine;

namespace SplitSpheres.Framework.GUI.Scripts
{
    /// <summary>
    /// A Simple GUI Panel
    /// </summary>
    public class GUIPanel : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected bool startActive = false;

        private void Awake()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

            if (startActive) Show();
            else Hide();
        }

        public virtual void Show()
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }


        public virtual void Hide()
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }
}