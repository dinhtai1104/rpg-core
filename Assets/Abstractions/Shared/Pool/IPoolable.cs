namespace Assets.Abstractions.Shared.Pool
{
    public interface IPoolable
    {
        void OnRecycled();
        bool IsRecycled { get; set; }
    }
}
