using SplitSpheres.Core.GUI.General;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

namespace UnityAdsIntegration
{
   
[RequireComponent (typeof (Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener {

    #if UNITY_IOS
    private string gameId = "3974738";
    #elif UNITY_ANDROID
    private string gameId = "3974739";
    #endif

    Button myButton;
    public string myPlacementId = "rewardedVideo";

    private GUIPanelAds guiPanelAds;
    
    void Start () {   
        myButton = GetComponent <Button> ();
        guiPanelAds = GetComponentInParent<GUIPanelAds>();
            
        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady (myPlacementId); 

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener (ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener (this);
        Advertisement.Initialize (gameId, true);
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo () {
        Advertisement.Show (myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId) {        
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
          guiPanelAds.Hide();
          Advertisement.RemoveListener(this);
        } else if (showResult == ShowResult.Skipped) {
            // Do not reward the user for skipping the ad.
        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    } 
} 
}