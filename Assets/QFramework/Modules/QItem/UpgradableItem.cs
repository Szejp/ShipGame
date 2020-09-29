namespace QFramework.Modules.QItem
{
    public abstract class UpgradableItem : ItemBase
    {
        public virtual bool CanUpgrade => true;
    }
}