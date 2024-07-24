namespace Assets.Abstractions.RPG.Units
{
    public interface ICharacter
    {
        TEngine GetEngine<TEngine>() where TEngine : IEngine;
    }
}
