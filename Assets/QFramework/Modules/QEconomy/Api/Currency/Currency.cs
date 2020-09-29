using System;
using QFramework.Modules.QItem.Api;
using UnityEngine;

namespace QFramework.Modules.QEconomy.Api.Currency
{
    [Serializable]
    public class Currency : IAmountable
    {
        [SerializeField] string id;
        [SerializeField] int amount;

        public event Action<Currency> OnChanged = currency => { };

        public int Amount
        {
            get => amount;
            set
            {
                amount = Mathf.Max(0, value);
                OnChanged?.Invoke(this);
            }
        }

        public string Id => id;

        public Currency(string id, int amount)
        {
            this.id = id;
            this.amount = amount;
        }

        public int GetAmount()
        {
            return Amount;
        }
    }
}