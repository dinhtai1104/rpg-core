namespace Assets.Abstractions.RPG.Items
{
    public class BaseRuntimeItem
    {
        private int _id;

        private string _description;
        private string _title;

        public int Id
        { 
            set => _id = value; 
            get => _id; 
        }
        public string Description
        {
            set => _description = value;
            get => _description;
        }
        public string Title
        {
            set => _title = value;
            get => _title;
        }

        public BaseRuntimeItem() { }
        public BaseRuntimeItem(int id) { }
        public BaseRuntimeItem(string parseData) { }
    }
}
