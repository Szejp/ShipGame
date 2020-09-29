using QFramework.Modules.QConfig.Scripts;
using UnityEngine;

namespace QFramework.Modules.QAds.Scripts.Config
{
    [CreateAssetMenu(fileName = "AdsConfig", menuName = "Q/Config/AdsConfig")]
    public class AdsConfig : ConfigBase
    {
        public static AdsConfig instance;

        public GameObject testAdsView;
        public bool testAds;
        public int interstitialAdInterval = 1;
        public UnityAdsSettings unityAdsSettings;

        public bool TestAds => testAds;
        
        #if Firebase_config

        public override void Setup()
        {
            base.Setup();
            instance = this;
        }

#endif
    }
}