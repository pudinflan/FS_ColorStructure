using SplitSpheres.Framework.GUI.Scripts;
using TMPro;
using UnityAdsIntegration;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SplitSpheres.Core.GUI.General
{
    public class GUIPanelAds : GUIPanel
    {

        public TextMeshProUGUI WinText;
        public TextMeshProUGUI rewardAmountText;

        public RewardedAdsButton rewardedAdsButton;

        protected override void Awake()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

            if (startActive) Show();

            EnableAds(false);


        }

        public void RewardAndShow(int rewardAmount, bool isWin)
        {
            rewardAmountText.text = rewardAmount.ToString();
            WinText.text = isWin ? "LEVEL WIN" : "TRY AGAIN";
            
            EnableAds(true);
            Show();
        }
        
        public override void Hide()
        {
            base.Hide();

            EnableAds(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void EnableAds(bool enable)
        {
            if (rewardedAdsButton != null)
            {
                rewardedAdsButton.enabled = enable;
            }
        }

    }
}
