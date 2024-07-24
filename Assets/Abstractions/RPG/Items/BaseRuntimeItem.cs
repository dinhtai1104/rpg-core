using Assets.Abstractions.RPG.Misc;
using System;

namespace Assets.Abstractions.RPG.Items
{
    public abstract class BaseRuntimeItem
    {
        private int _id;
        private string _icon;

        private string _description;
        private string _title;
        public abstract ERuntimeItem RuntimeItemType { get; }

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

        public string Icon
        {
            set => _icon = value;
            get => _icon;
        }
        public virtual string PathUIIcon => $"Icon_{Icon}";

        public virtual string PathUIPrefab => $"UI{RuntimeItemType}Item";

        public BaseRuntimeItem() { }
        public BaseRuntimeItem(int id) { }
        public BaseRuntimeItem(string parseData) { }

        public virtual void Drop()
        {

        }

        public virtual bool IsSame(BaseRuntimeItem other)
        {
            if (other.GetType() != GetType()) return false;
            return other.Id == Id && other.RuntimeItemType == RuntimeItemType;
        }

        public virtual void Add(BaseRuntimeItem other)
        {

        }
    }
}
