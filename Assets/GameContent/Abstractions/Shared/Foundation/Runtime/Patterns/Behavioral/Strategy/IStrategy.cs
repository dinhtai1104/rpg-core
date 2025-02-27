namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Strategy interface. </summary>
    public interface IStrategy
    {
        /// <summary> Execute the strategy. </summary>
        void OnExecute();
    }

    /// <summary> Strategy interface. </summary>
    public interface IStrategy<out R>
    {
        /// <summary> Execute the strategy. </summary>
        R OnExecute();
    }

    /// <summary> Strategy interface with one parameter. </summary>
    public interface IStrategy<out R, in T>
    {
        /// <summary> Execute the strategy. </summary>
        R OnExecute(T value);
    }

    /// <summary> Strategy interface with two parameters. </summary>
    public interface IStrategy<out R, in T0, in T1>
    {
        /// <summary> Execute the strategy. </summary>
        R OnExecute(T0 value0, T1 value1);
    }

    /// <summary> Strategy interface with three parameters. </summary>
    public interface IStrategy<out R, in T0, in T1, in T2>
    {
        /// <summary> Execute the strategy. </summary>
        R OnExecute(T0 value0, T1 value1, T2 value2);
    }
}