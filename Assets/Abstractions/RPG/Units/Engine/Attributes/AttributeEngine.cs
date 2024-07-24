using Assets.Abstractions.RPG.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.RPG.Units.Engine.Attributes
{
    public class AttributeEngine : BaseMonoEngine, IAttributeGroup
    {
#if UNITY_EDITOR
        [SerializeField] private List<AttributeInspectorData> _attributeDebugger;
#endif

        private Dictionary<string, AttributeData> _attrDict;

        public int TotalStat => _attrDict.Count;

        public IEnumerable<string> StatNames => _attrDict.Keys;

#if UNITY_EDITOR || DEVELOPMENT || DEVELOPMENT_BUILD
        public Dictionary<string, AttributeData> AttributesDirectDict => _attrDict;
#endif
        public override void Initialize()
        {
            base.Initialize();
            _attrDict = new Dictionary<string, AttributeData>();
        }
        public IAttributeGroup SetMinValue(string name, float min)
        {
            if (_attrDict.ContainsKey(name)) _attrDict[name].SetConstraintMin(min);
            return this;
        }

        public IAttributeGroup SetMaxValue(string name, float max)
        {
            if (_attrDict.ContainsKey(name)) _attrDict[name].SetConstraintMax(max);
            return this;
        }

        public void AddStat(string name, float baseValue, float min = float.MinValue, float max = float.MaxValue)
        {
            if (_attrDict.ContainsKey(name))
            {
                //                Debug.LogError("Duplicated attribute " + name);
            }
            else
            {
                AttributeData stat = new AttributeData { BaseValue = baseValue };
                stat.SetConstraintMin(min);
                stat.SetConstraintMax(max);

                _attrDict.Add(name, stat);

#if UNITY_EDITOR
                if (_attributeDebugger == null) _attributeDebugger = new List<AttributeInspectorData>();
                var data = new AttributeInspectorData(name, stat);
                _attributeDebugger.Add(data);
#endif
            }
        }

        public AttributeData CreateStat(string name, float baseValue, float min = float.MinValue, float max = float.MaxValue)
        {
            if (_attrDict.ContainsKey(name)) return null;
            AttributeData stat = new AttributeData { BaseValue = baseValue };
            stat.SetConstraintMin(min);
            stat.SetConstraintMax(max);

            _attrDict.Add(name, stat);

#if UNITY_EDITOR
            if (_attributeDebugger == null) _attributeDebugger = new List<AttributeInspectorData>();
            var data = new AttributeInspectorData(name, stat);
            _attributeDebugger.Add(data);
#endif
            return stat;
        }

        public void AddListener(string name, Action<float> callback)
        {
            if (!_attrDict.ContainsKey(name)) return;
            AttributeData stat = _attrDict[name];
            stat.AddListener(callback);
        }

        public void RemoveListener(string name, Action<float> callback)
        {
            if (!_attrDict.ContainsKey(name)) return;
            _attrDict[name].RemoveListener(callback);
        }

        public bool HasStat(string name)
        {
            return _attrDict.ContainsKey(name);
        }

        public bool HasModifier(string statName, AttributeModifier modifier)
        {
            if (!HasStat(statName)) return false;
            return _attrDict[statName].HasModifier(modifier);
        }

        public float GetTemporaryValue(string statName, float baseValue)
        {
            return !_attrDict.ContainsKey(statName) ? baseValue : _attrDict[statName].CalculateTemporaryValue(baseValue);
        }

        /// <summary>
        /// Set base value for attribute. Must set callUpdater=false when called in UpdateAttributes method of updaters to prevent stack overflow.
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Base value</param>
        /// <param name="callUpdater">Decide if attribute updaters are notified by this change</param>
        public IAttributeGroup SetBaseValue(string name, float value, bool callUpdater = true)
        {
            if (_attrDict != null && _attrDict.ContainsKey(name))
            {
                _attrDict[name].BaseValue = value;
            }
            else
            {
                //Debug.Log(gameObject.name + " " + name + " is not in attribute dictionary");
            }

            return this;
        }

        public float GetBaseValue(string name, float defaultValue = 0f)
        {
            if (_attrDict == null) return 0f;

            if (_attrDict.ContainsKey(name)) return _attrDict[name].BaseValue;

            //            Debug.LogError(name + " is not in attribute dictionary");
            return defaultValue;
        }

        public float GetValue(string name, float defaultValue = 0f)
        {
            if (_attrDict == null) return defaultValue;

            return _attrDict.ContainsKey(name) ? _attrDict[name].Value : defaultValue;
        }

        public float GetLastValue(string statName, float defaultValue = 0f)
        {
            if (_attrDict == null) return defaultValue;

            return _attrDict.ContainsKey(statName) ? _attrDict[statName].LastValue : defaultValue;
        }

        public IEnumerable<AttributeModifier> GetModifiers(string statName)
        {
            if (_attrDict == null || !_attrDict.ContainsKey(statName)) return Enumerable.Empty<AttributeModifier>();

            return _attrDict[statName].Modifiers;
        }

        public float GetMinConstraint(string name)
        {
            if (_attrDict == null) return 0f;

            return _attrDict.ContainsKey(name) ? _attrDict[name].ConstraintMin : 0f;
        }

        public float GetMaxConstraint(string name)
        {
            if (_attrDict == null) return float.MaxValue;

            return _attrDict.ContainsKey(name) ? _attrDict[name].ConstraintMax : float.MaxValue;
        }

        public void AddModifier(string statName, AttributeModifier mod, object source)
        {
            if (string.IsNullOrEmpty(statName) || !_attrDict.ContainsKey(statName))
            {
                //Debug.Log(gameObject.name + " Attribute key does not exist: " + name);
            }
            else
            {
                AttributeData attr = _attrDict[statName];
                mod.Source = source;
                attr.AddModifier(mod);
            }
        }

        public void RemoveModifier(string name, AttributeModifier mod)
        {
            if (_attrDict.ContainsKey(name))
            {
                AttributeData attr = _attrDict[name];
                attr.RemoveModifier(mod);
            }
        }

        public void RemoveModifiersFromSource(object source)
        {
            foreach (var key in _attrDict.Keys)
            {
                _attrDict[key].RemoveAllModifiersFromSource(source);
            }
        }

        public void ClearAllModifiers()
        {
            foreach (var stat in _attrDict.Values)
            {
                stat.ClearModifiers();
            }
        }

        public int CountModifierFromSource(string name, object source)
        {
            return !_attrDict.ContainsKey(name) ? 0 : _attrDict[name].CountModifiersFromSource(source);
        }

        public void Copy(IAttributeGroup statGroup, float percentage = 1f)
        {
            foreach (var attributeName in statGroup.StatNames)
            {
                if (HasStat(attributeName))
                {
                    SetBaseValue(attributeName, statGroup.GetBaseValue(attributeName) * percentage);
                }
                else
                {
                    AddStat(attributeName, statGroup.GetBaseValue(attributeName), statGroup.GetMinConstraint(attributeName), statGroup.GetMaxConstraint(attributeName));
                }

                foreach (var mod in statGroup.GetModifiers(attributeName))
                {
                    AddModifier(attributeName, mod, mod.Source);
                }
            }
        }

        public void Copy(IAttributeGroup statGroup, string[] _ignoreKeys, float percentage = 1f)
        {
            foreach (var attributeName in statGroup.StatNames)
            {
                var ignore = false;

                foreach (var key in _ignoreKeys)
                {
                    if (attributeName != key) continue;
                    ignore = true;
                    break;
                }

                if (HasStat(attributeName) && !ignore)
                {
                    SetBaseValue(attributeName, statGroup.GetBaseValue(attributeName) * percentage);
                }
            }
        }

        public void CalculateStats()
        {
            foreach (var stat in _attrDict.Values)
            {
                stat.RecalculateValue();
            }
        }

        public Dictionary<string, float> ConvertToDictionary()
        {
            var dict = new Dictionary<string, float>();
            foreach (var pair in _attrDict)
            {
                dict.Add(pair.Key, pair.Value.Value);
            }

            return dict;
        }
    }
}