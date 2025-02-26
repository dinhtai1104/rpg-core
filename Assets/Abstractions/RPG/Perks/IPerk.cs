
namespace Assets.Abstractions.RPG.Perks
{
    public interface IPerk
    {
        string GetLocalize();
    }

    public interface IPerkAction
    {
        void Action();
    }
}
