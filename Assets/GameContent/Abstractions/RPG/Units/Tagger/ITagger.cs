using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.RPG.Units
{
    public interface ITagger
    {
        int Id { get; }
        IList<string> Tags { get; }
        void AddTag(string tag);
        void RemoveTag(string tag);
        bool HasTag(string tag);
        bool HasTags(IEnumerable<string> tags);
        bool HasAnyOfTags(IEnumerable<string> tags);
        bool HasAllOfTags(IEnumerable<string> tags);
        void ClearAll();
    }
}
