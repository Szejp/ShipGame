using System;
using QFramework.Modules.QAds.Scripts.Config;
using UnityEngine;
using UnityEngine.Advertisements;

namespace QFramework.Modules.QAds.Scripts.Implementation
{
    public class Rewarded : IUnityAdsListener
    {
#if UNITY_ANDROID
        const string unityAdId = "rewardedVideo";
#elif UNITY_IPHONE
        const string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        const string unityAdId = "unexpected_platform";
#endif

        // RewardBasedVideoAd rewardBasedVideo;
        Action onAdSucced;
        Action onAdFailed;

        public bool IsReady => Advertisement.IsReady(UnityAdUnit);

        string UnityAdUnit => AdsConfig.instance.unityAdsSettings.rewardedAdUnit;

        public static Action OnReady;

        public Rewarded()
        {
//            // Get singleton reward based video ad reference.
//            rewardBasedVideo = RewardBasedVideoAd.Instance;
//
//            // Called when an ad request has successfully loaded.
//            rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
//            // Called when an ad request failed to load.
//            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
//            // Called when an ad is shown.
//            rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
//            // Called when the ad starts to play.
//            rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
//            // Called when the user should be rewarded for watching a video.
//            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
//            // Called when the ad is closed.
//            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
//            // Called when the ad click caused the user to leave the application.
//            rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

            Advertisement.AddListener(this);
            RequestRewardBasedVideo();
        }

        public void RequestRewardBasedVideo()
        {
//            // Create an empty ad request.
//            AdRequest request = new AdRequest.Builder().Build();
//            // Load the rewarded video ad with the request.
//            rewardBasedVideo.LoadAd(request, adUnitId);
        }

        public void Show(Action onSuccess, Action onFailure)
        {
            onAdSucced = onSuccess;
            onAdFailed = onFailure;

            if (Advertisement.IsReady(UnityAdUnit))
            {
                Advertisement.Show(UnityAdUnit);
                Debug.LogFormat("[Rewarded] show");
            }
        }

//
//        void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
//        {
//            Debug.Log("HandleRewardBasedVideoLoaded event received");
//        }
//
//        void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//        {
//            Debug.Log("HandleRewardBasedVideoFailedToLoad event received with message: "
//                      + args.Message);
//        }
//
//        void HandleRewardBasedVideoOpened(object sender, EventArgs args)
//        {
//            Debug.Log("HandleRewardBasedVideoOpened event received");
//        }
//
//        void HandleRewardBasedVideoStarted(object sender, EventArgs args)
//        {
//            Debug.Log("HandleRewardBasedVideoStarted event received");
//        }
//
//        void HandleRewardBasedVideoClosed(object sender, EventArgs args)
//        {
//            RequestRewardBasedVideo();
//            Debug.Log("HandleRewardBasedVideoClosed event received");
//        }
//
//        void HandleRewardBasedVideoRewarded(object sender, Reward args)
//        {
//            string type = args.Type;
//            double amount = args.Amount;
//
//            onAdSucced?.Invoke();
//            onAdSucced = null;
//
//            Debug.Log("HandleRewardBasedVideoRewarded event received for " + amount + " " + type);
//        }
//
//        void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
//        {
//            Debug.Log("HandleRewardBasedVideoLeftApplication event received");
//        }
        // Implement IUnityAdsListener interface methods:
        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                onAdSucced?.Invoke();
                onAdSucced = null;
                // Reward the user for watching the ad to completion.
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
            }

            Debug.LogFormat("[Unity Ads] OnUnityAdsDidFinish");
        }

        public void OnUnityAdsReady(string placementId)
        {
            // If the ready Placement is rewarded, show the ad:
            if (placementId == unityAdId)
                OnReady?.Invoke();

            Debug.LogFormat("[Unity Ads] OnUnityAdsReady");
        }

        public void OnUnityAdsDidError(string message)
        {
            Debug.LogError("[Rewarded] " + message);
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            Debug.LogFormat("[Unity Ads] OnUnityAdsDidStart");
        }
    }
}