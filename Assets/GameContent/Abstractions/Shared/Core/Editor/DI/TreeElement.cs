using System;
using System.Collections.Generic;

namespace Assets.Abstractions.Shared.Core.DI.Editors
{
	[Serializable]
	public class TreeElement
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Depth { get; set; }
		public TreeElement Parent { get; set; }
		public List<TreeElement> Children { get; set; } = new List<TreeElement>();
		public bool HasChildren => Children != null && Children.Count > 0;

		public TreeElement() { }

		public TreeElement(string name, int depth, int id)
		{
			Name = name;
			Id = id;
			Depth = depth;
		}

		public void SetParent(TreeElement parent)
		{
			Parent?.Children.Remove(this);
			Parent = parent;
			Parent?.Children.Add(this);
		}
	}
}
