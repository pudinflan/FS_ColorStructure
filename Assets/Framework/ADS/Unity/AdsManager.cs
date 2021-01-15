using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

// ReSharper disable once CheckNamespace
namespace UnityAdsIntegration
{
    public class AdsManager : MonoBehaviour
    {
        const string PlayStoreGameID = "3974739";
        const string VideoAdPlacementID = "video";
        const string PlacementId = "banner";

        [SerializeField] bool testMode = true;

        private void Start()
        {
            Advertisement.Initialize(PlayStoreGameID, testMode);
            StartCoroutine(ShowBannerWhenInitialized());
        }

        public static void ShowVideoAd()
        {
            // Check if UnityAds ready before calling Show method:
            if (Advertisement.IsReady())
            {
                Advertisement.Show(VideoAdPlacementID);
            }
            else
            {
                Debug.Log(VideoAdPlacementID + "not ready at the moment! Please try again later!");
            }
        }
        

        IEnumerator ShowBannerWhenInitialized () {
            while (!Advertisement.isInitialized) {
                yield return new WaitForSeconds(0.5f);
            }
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show (PlacementId);
           
            Debug.Log("Banner Ititialzied");
        }
    }   
}