namespace Assets.Abstractions.RPG.Units.Experience
{
    [System.Serializable]
    public class ExperienceData
    {
        private int _exp;
        public int Exp
        {
            get => _exp;
            set => _exp = value;
        }
    }
}
