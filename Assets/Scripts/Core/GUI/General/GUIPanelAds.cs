using SplitSpheres.Framework.GUI.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SplitSpheres.Core.GUI.General
{
    public class GUIPanelAds : GUIPanel
    {

        public TextMeshProUGUI rewardAmountText;


        protected override void Awake()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

            if (startActive) Show();
         
        }

   
        public void RewardAndShow(int rewardAmount)
        {
            rewardAmountText.text = rewardAmount.ToString();
            Show();
        }
        
        public override void Hide()
        {
            base.Hide();

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

      
    }
}
