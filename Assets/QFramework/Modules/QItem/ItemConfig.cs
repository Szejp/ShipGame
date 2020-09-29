using System.Collections.Generic;
using QFramework.Modules.QEconomy.Api;
using QFramework.Modules.QEconomy.Api.Currency;
using UnityEngine;

namespace QFramework.Modules.QItem
{
    public class ItemBaseConfig : ScriptableObject
    {
        public List<UpgradableItem> itemLevels = new List<UpgradableItem>();
        public int CurrentLevel { get; private set; }

        public UpgradableItem GetItem(int level)
        {
            if (itemLevels.Count < level)
            {
                Debug.LogError("[ItemConfig] You are trying to get an item with a higher level than possible");
                return null;
            }


            return itemLevels[level - 1];
        }

        public Currency GetPrice(int level)
        {
            return GetItem(level).price;
        }

        public Currency GetUpradePrice()
        {
            return null;
        }

        public void TryUpgrade()
        {
            if (GetItem(CurrentLevel).CanUpgrade)
                CurrentLevel++;
        }
    }
}