using System;
using QFramework.Modules.QEconomy.Api;
using QFramework.Modules.QEconomy.Api.Currency;

namespace QFramework.Modules.QEconomy
{
    public static class Economy
    {
        public static event Action<Currency> OnCurrencyChanged = currency => { };

        static EconomyData economyData;

        public static void Init(EconomyData economyData)
        {
            if (economyData == null)
                return;
            
            Economy.economyData = economyData;

            foreach (var c in economyData.currencies)
                c.OnChanged += OnCurrencyChangedHandler;
        }

        public static void AddCurrency(Currency currency)
        {
            ModifyCurrency(currency.Id, currency.Amount);
        }

        public static void RemoveCurrency(string id, int amount)
        {
            ModifyCurrency(id, -amount);
        }

        public static void ModifyCurrency(CurrencyType currencyType, int amout)
        {
            ModifyCurrency(currencyType.ToString(), amout);
        }

        public static void HasCurrency(CurrencyType currencyType, int amount)
        {
            HasCurrency(currencyType.ToString(), amount);
        }
        
        public static bool HasCurrency(string id, int amount)
        {
            var selectedCurrency = economyData.GetCurrency(id);

            if (selectedCurrency != null)
                return selectedCurrency.Amount >= amount;

            return false;
        }

        public static Currency GetCurrency(string id)
        {
            return economyData.GetCurrency(id);
        }

        public static Currency GetCurrency(CurrencyType currencyType)
        {
            return GetCurrency(currencyType.ToString());
        }
        
        static void ModifyCurrency(string id, int amount)
        {
            var selectedCurrency = GetCurrency(id);

            if (selectedCurrency != null)
                selectedCurrency.Amount += amount;
        }

        static void OnCurrencyChangedHandler(Currency currency)
        {
            OnCurrencyChanged?.Invoke(currency);
        }
    }
}