#if EASY_MOBILE && EASY_MOBILE_PRO && EM_UIAP

using System.Linq;
using QFramework.Modules.QConfig.Scripts;
using QFramework.Modules.QOffers;
using UnityEngine;

namespace QFramework.Modules.Shared
{
    public static class IapPurchaseToOfferMediator
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            InAppPurchasing.PurchaseCompleted += PurchaseCompletedHandler;
        }

        static void PurchaseCompletedHandler(IAPProduct product)
        {
            var config = Configs.GetConfig<OffersConfig>();

            if (config == null)
                Debug.LogError("[IapPurchaseToOfferMediator] Offer config is null");

            if (config.offers == null)
                Debug.LogError("[IapPurchaseToOfferMediator] Offer config is null");

            var offer = config.offers.FirstOrDefault(p => p.id.Equals(product.Name));

            if (offer == null)
                Debug.LogError("[IapPurchaseToOfferMediator] Offer is null");

            offer.Claim();

            Debug.LogFormat("[IapPurchaseToOfferMediator] {0}", product.Name);
        }
    }
}

#endif