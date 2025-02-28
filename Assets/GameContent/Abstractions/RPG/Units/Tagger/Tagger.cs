using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units
{
    [Serializable, InlineProperty]
    public class Tagger : MonoBehaviour, ITagger, ISerializationCallbackReceiver
    {
        [SerializeField] private List<string> m_Tags;

        public int Id { get; }
        public IList<string> Tags => m_Tags;

        private HashSet<string> m_HashTags;

        private void Awake()
        {
            m_Tags = Enumerable.Empty<string>().ToList();
            m_HashTags = new HashSet<string>();
        }

        public void AddTag(string tag)
        {
            if (m_HashTags.Contains(tag)) return;
            m_Tags.Add(tag);
            m_HashTags.Add(tag);
        }

        public void RemoveTag(string tag)
        {
            if (!m_HashTags.Contains(tag)) return;
            m_Tags.Remove(tag);
            m_HashTags.Remove(tag);
        }

        public bool HasTag(string tag)
        {
            return m_HashTags.Contains(tag);
        }

        public bool HasTags(IEnumerable<string> tags)
        {
            return m_HashTags.IsSupersetOf(tags);
        }

        public bool HasAnyOfTags(IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                if (m_HashTags.Contains(tag)) return true;
            }

            return false;
        }

        public bool HasAllOfTags(IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                if (!m_HashTags.Contains(tag)) return false;
            }

            return true;
        }

        public void OnBeforeSerialize()
        {
        }

        public void ClearAll()
        {
            m_Tags.Clear();
            m_HashTags.Clear();
        }
        public void OnAfterDeserialize()
        {
            if (m_Tags == null) return;

            if (m_HashTags == null) m_HashTags = new HashSet<string>();

            foreach (var tag in m_Tags)
            {
                m_HashTags.Add(tag);
            }
        }
    }
}