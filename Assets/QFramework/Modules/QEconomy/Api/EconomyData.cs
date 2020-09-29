using System;
using System.Collections.Generic;
using System.Linq;

namespace QFramework.Modules.QEconomy.Api
{
    [Serializable]
    public class EconomyData
    {
        public List<Currency.Currency> currencies;

        public event Action<Currency.Currency> OnCurrencyChanged;

        public Currency.Currency GetCurrency(string id)
        {
            return currencies.FirstOrDefault(p => p.Id.Equals(id));
        }
    }
}