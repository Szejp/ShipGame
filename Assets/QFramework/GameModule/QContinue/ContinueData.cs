using System;
using System.Collections.Generic;
using QFramework.Helpers;
using QFramework.Modules.QEconomy.Api;
using QFramework.Modules.QEconomy.Api.Currency;
using UnityEngine;

namespace QFramework.GameModule.QContinue
{
    [Serializable]
    public class ContinueData
    {
        [SerializeField]
        List<Currency> continueLevels;

        public Currency GetCurrencyForLevel(int level)
        {
            return continueLevels.IndexAtOrLast(level);
        }
    }
}