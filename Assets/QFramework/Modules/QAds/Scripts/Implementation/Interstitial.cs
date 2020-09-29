using QFramework.Modules.QAds.Scripts.Config;
using UnityEngine.Advertisements;

namespace QFramework.Modules.QAds.Scripts.Implementation
{
    public class Interstitial
    {
#if UNITY_ANDROID
        const string unityAdId = "video";
#elif UNITY_IPHONE
        const string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        const string adUnitId = "unexpected_platform";
#endif

        //InterstitialAd interstitial;

        string UnityAdUnit => AdsConfig.instance.unityAdsSettings.interstitialAdUnit;

        public Interstitial()
        {
//            // Initialize an InterstitialAd.
//            this.interstitial = new InterstitialAd(adUnitId);
//            // Called when an ad request has successfully loaded.
//            this.interstitial.OnAdLoaded += HandleOnAdLoaded;
//            // Called when an ad request failed to load.
//            this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
//            // Called when an ad is shown.
//            this.interstitial.OnAdOpening += HandleOnAdOpened;
//            // Called when the ad is closed.
//            this.interstitial.OnAdClosed += HandleOnAdClosed;
//            // Called when the ad click caused the user to leave the application.
//            this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        }

        public void Show()
        {
            if (Advertisement.IsReady(UnityAdUnit))
                Advertisement.Show(UnityAdUnit);
        }

        public void RequestInterstitial()
        {
//            // Create an empty ad request.
//            AdRequest request = new AdRequest.Builder().Build();
//            // Load the interstitial with the request.
//            this.interstitial.LoadAd(request);
        }

//        void HandleOnAdLoaded(object sender, EventArgs args)
//        {
//            Debug.Log("HandleAdLoaded event received");
//        }
//
//        void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//        {
//            Debug.Log("HandleFailedToReceiveAd event received with message: "
//                      + args.Message);
//        }
//
//        void HandleOnAdOpened(object sender, EventArgs args)
//        {
//            Debug.Log("HandleAdOpened event received");
//        }
//
//        void HandleOnAdClosed(object sender, EventArgs args)
//        {
//            Debug.Log("HandleAdClosed event received");
//        }
//
//        void HandleOnAdLeavingApplication(object sender, EventArgs args)
//        {
//            Debug.Log("HandleAdLeavingApplication event received");
//        }
    }
}