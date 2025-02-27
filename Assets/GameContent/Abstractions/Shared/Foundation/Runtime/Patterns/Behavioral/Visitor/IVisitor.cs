namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Visitor interface. </summary>
    public interface IVisitor { }

    /// <summary> Visitor interface. </summary>
    public interface IVisitor<in T> where T : IVisitable
    {
        /// <summary> Visit to a visitor. </summary>
        void Visit(T visit);
    }
}