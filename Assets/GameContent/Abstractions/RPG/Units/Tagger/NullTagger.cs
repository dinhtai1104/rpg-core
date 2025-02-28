using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.RPG.Units
{
    public class NullTagger : ITagger
    {
        public int Id => 0;

        public IList<string> Tags => null;

        public void AddTag(string tag)
        {
        }

        public void ClearAll()
        {
        }

        public bool HasAllOfTags(IEnumerable<string> tags)
        {
            return false;
        }

        public bool HasAnyOfTags(IEnumerable<string> tags)
        {
            return false;
        }

        public bool HasTag(string tag)
        {
            return false;
        }

        public bool HasTags(IEnumerable<string> tags)
        {
            return false;
        }

        public void RemoveTag(string tag)
        {
        }
    }
}
