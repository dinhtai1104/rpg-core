namespace Assets.Abstractions.Shared.Pool.Factory
{
    public interface IObjectFactory<T>
    {
        T Create();
    }
}
