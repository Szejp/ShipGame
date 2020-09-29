using QFramework.Modules.QEconomy.Api;
using QFramework.Modules.QEconomy.Api.Currency;
using QFramework.Modules.QItem.Api;

namespace QFramework.Modules.QItem
{
    public abstract class ItemBase : IClaimable
    {
        public string id;
        public Currency price;

        public abstract void Claim();
        public abstract void TryClaim();
    }
}