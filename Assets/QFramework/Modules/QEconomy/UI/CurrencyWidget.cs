using QFramework.Helpers.UI;
using QFramework.Helpers.UI.ValueWidget;
using QFramework.Modules.QEconomy.Api;
using QFramework.Modules.QEconomy.Api.Currency;
using UnityEngine;

namespace QFramework.Modules.QEconomy.UI {
    public class CurrencyWidget : IntValueWidget
    {
        [SerializeField] CurrencyType currencyType;

        Currency currency;

        void Awake()
        {
            Economy.OnCurrencyChanged += TryRefresh;
            SetValue(Economy.GetCurrency(currencyType).Amount);
        }

        void OnDestroy()
        {
            Economy.OnCurrencyChanged -= TryRefresh;
        }

        void TryRefresh(Currency currency)
        {
            if (currencyType.ToString().Equals(currency.Id))
                SetValue(currency.Amount);
        }
    }
}