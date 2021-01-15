using SplitSpheres.Core.Gameplay;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace SplitSpheres.Core.GUI.General
{
    [RequireComponent(typeof(Button))]
    public class GUIPanelRetryRewarded : MonoBehaviour,IUnityAdsListener
    {
#if UNITY_IOS
    private string gameId = "3974738";
#elif UNITY_ANDROID
        private string gameId = "3974739";
#endif
        public GameManager gameManager;

        Button myButton;
        public string myPlacementId = "rewardedVideo";

        private GUIPanelRetry guiPanelRetry;
        void Start()
        {
            myButton = GetComponent<Button>();
            guiPanelRetry = GetComponentInParent<GUIPanelRetry>();
            gameManager = FindObjectOfType<GameManager>();
            
            // Set interactivity to be dependent on the Placement’s status:
            myButton.interactable = Advertisement.IsReady(myPlacementId);

            // Map the ShowRewardedVideo function to the button’s click listener:
            if (myButton) myButton.onClick.AddListener(ShowRewardedVideoRetry);

            // Initialize the Ads listener and service:
         
            Advertisement.AddListener(this);
        
        }

        // Implement a function for showing a rewarded video ad:
        void ShowRewardedVideoRetry()
        {
            Advertisement.Show(myPlacementId);
        }

        // Implement IUnityAdsListener interface methods:
        public void OnUnityAdsReady(string placementId)
        {
            // If the ready Placement is rewarded, activate the button: 
            if (placementId == myPlacementId)
            {
                myButton.interactable = true;
            }
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                guiPanelRetry.ResumeGameWithBalls();
                Advertisement.RemoveListener(this);
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
            }
        }

        public void OnUnityAdsDidError(string message)
        {
            // Log the error.
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            // Optional actions to take when the end-users triggers an ad.
        }
    }
}