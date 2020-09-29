namespace QFramework.Modules.QItem.Api
{
    public interface IClaimable
    {
        void Claim();
        void TryClaim();
    }
}