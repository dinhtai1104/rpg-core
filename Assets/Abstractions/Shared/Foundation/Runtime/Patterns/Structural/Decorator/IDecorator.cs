namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Operation that can be altered by decorators. </summary>
    public interface IDecorator
    {
        /// <summary> The operation to be executed. </summary>
        void Operation();
    }

    /// <summary> Operation that can be altered by decorators. </summary>
    public interface IDecorator<out R>
    {
        /// <summary> The operation to be executed. </summary>
        R Operation();
    }

    /// <summary> Operation that can be altered by decorators. </summary>
    public interface IDecorator<out R, in T>
    {
        /// <summary> The operation to be executed. </summary>
        R Operation(T value);
    }

    /// <summary> Operation that can be altered by decorators. </summary>
    public interface IDecorator<out R, in T0, in T1>
    {
        /// <summary> The operation to be executed. </summary>
        R Operation(T0 value0, T1 value1);
    }
}