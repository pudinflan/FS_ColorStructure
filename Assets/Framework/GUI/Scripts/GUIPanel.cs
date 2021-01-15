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

        protected virtual void Awake()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

            if (startActive) Show();
            else Hide();
        }

        public virtual void Show()
        {
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1;
            }
        
        }


        public virtual void Hide()
        {
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0;
            }
           
        }
    }
}