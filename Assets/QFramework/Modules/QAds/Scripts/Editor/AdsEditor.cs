using System;
using QFramework.Modules.QAds.Scripts.Api;
using UnityEditor;

namespace QFramework.Modules.QAds.Scripts.Editor
{
#if UNITY_EDITOR
    public class AdsEditor : UnityEditor.Editor
    {
        [MenuItem("Ads/ShowInterstitial")]
        public static void ShowInterstitial()
        {
            Ads.ShowInterstitial();
        }

        [MenuItem("Ads/ShowRewarded")]
        public static void ShowRewarded(Action onSuccess, Action onFailure)
        {
            Ads.ShowRewarded(onSuccess, onFailure);
        }

        [MenuItem("Ads/ShowBanner")]
        public static void ShowBanner()
        {
            Ads.ShowBanner();
        }
    }
#endif
}