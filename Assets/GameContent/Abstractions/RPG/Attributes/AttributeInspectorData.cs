using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Assets.Abstractions.RPG.Attributes
{
    [Serializable]
    public class AttributeInspectorData
    {
        public static readonly AttributeInspectorData NullAttribute = new AttributeInspectorData();

        [HideInInspector, SerializeField] private string _editorName;


        [SerializeField, ReadOnly]
        private string _name;

        [SerializeField]
        private AttributeData stat;

        public string Name
        {
            get { return _name; }
        }

        public AttributeData Stat
        {
            get { return stat; }
        }

        public AttributeInspectorData()
        {
        }

        public AttributeInspectorData(string name, AttributeData stat)
        {
            _name = name;
            _editorName = name;
            this.stat = stat;
        }
    }
}