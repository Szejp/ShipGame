using UnityEngine;

namespace QFramework.Modules.Purchases {
    public static class Purchasing
    {
        public static void Init()
        {

#if EASY_MOBILE && EASY_MOBILE_PRO && EM_UIAP

            if (InAppPurchasing.IsInitialized())
                return;

            InAppPurchasing.InitializePurchasing();
            InAppPurchasing.PurchaseCompleted += PurchaseCompletedHandler;
            InAppPurchasing.PurchaseFailed += PurchaseFailedHandler;
            
#endif
        }

#if EASY_MOBILE && EASY_MOBILE_PRO && EM_UIAP
        
        // Successful purchase handler
        static void PurchaseCompletedHandler(IAPProduct product)
        {
            // Compare product name to the generated name constants to determine which product was bought
            //      switch (product.Name)
            //       {
            //            case EM_IAPConstants.Sample_Product:
            Debug.LogFormat("[Purchasing] {0} was purchased. The user should be granted it now.", product.Name);
            //                break;
            //            case EM_IAPConstants.Another_Sample_Product:
            //                Debug.Log("Another_Sample_Product was purchased. The user should be granted it now.");
            //                break;
            // More products here...
            //     }
        }

        static void PurchaseFailedHandler(IAPProduct product)
        {
            Debug.LogError("[Purchasing] The purchase of product " + product.Name + " has failed.");
        }

#endif

    }
}