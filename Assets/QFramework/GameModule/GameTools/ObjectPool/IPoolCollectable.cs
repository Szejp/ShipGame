namespace QFramework.GameModule.GameTools.ObjectPool {
    public interface IPoolCollectable {
        void SetCollector(ICollector collector);
        void Collect();
    }
}
