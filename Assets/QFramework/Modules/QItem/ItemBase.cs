using QFramework.Modules.QEconomy.Api;
using QFramework.Modules.QEconomy.Api.Currency;
using QFramework.Modules.QItem.Api;
using IClaimable = DailyRewardSytem.IClaimable;

namespace QFramework.Modules.QItem
{
    public abstract class ItemBase : DailyRewardSytem.IClaimable
    {
        public string id;
        public Currency price;

        public abstract void Claim();
        public abstract void TryClaim();
    }
}