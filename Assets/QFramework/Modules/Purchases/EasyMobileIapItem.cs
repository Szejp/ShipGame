#if EASY_MOBILE && EASY_MOBILE_PRO && EM_UIAP

using QFramework.Modules.QItem;

namespace QFramework.Modules.Purchases
{
    public abstract class EasyMobileIapItem : ItemBase
    {
        void Init()
        {
            InAppPurchasing.PurchaseCompleted += PurchaseCompletedHandler;
        }

        void PurchaseCompletedHandler(IAPProduct product)
        {
            if (product.Name.Equals(id))
                Claim();
        }
    }
}

#endif