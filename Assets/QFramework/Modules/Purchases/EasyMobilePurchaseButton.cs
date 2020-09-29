#if EASY_MOBILE && EASY_MOBILE_PRO && EM_UIAP

using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Modules.Purchases
{
    [RequireComponent(typeof(Button))]
    public class EasyMobilePurchaseButton : MonoBehaviour
    {
        [SerializeField] string productName;
        [SerializeField] Button button;

        Button Button
        {
            get
            {
                if (button == null)
                    button = GetComponent<Button>();

                return button;
            }
        }

        void Awake()
        {
            Button.onClick.AddListener(Purchase);
        }

        void OnDestroy()
        {
            Button.onClick.RemoveListener(Purchase);
        }

        void Purchase()
        {
            InAppPurchasing.Purchase(productName);
        }
    }
}

#endif