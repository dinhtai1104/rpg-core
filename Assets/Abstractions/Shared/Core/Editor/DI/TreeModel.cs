using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Abstractions.Shared.Core.DI.Editors
{
	public class TreeModel<T> where T : TreeElement
	{
		private IList<T> _data;
		public T Root { get; private set; }

		public TreeModel(IList<T> data)
		{
			SetData(data);
		}

		public T Find(int id)
		{
			return _data.FirstOrDefault(element => element.Id == id);
		}

		private void SetData(IList<T> data)
		{
			Init(data);
		}

		private void Init(IList<T> data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "Input data is null. Ensure input is a non-null list.");
			}

			_data = data;
			if (_data.Count > 0)
			{
				Root = TreeElementUtility.ListToTree(data);
			}
		}

		public IList<int> GetAncestors(int id)
		{
			var parents = new List<int>();
			TreeElement T = Find(id);
			if (T != null)
			{
				while (T.Parent != null)
				{
					parents.Add(T.Parent.Id);
					T = T.Parent;
				}
			}

			return parents;
		}

		public IList<int> GetDescendantsThatHaveChildren(int id)
		{
			T searchFromThis = Find(id);
			if (searchFromThis != null)
			{
				return GetParentsBelowStackBased(searchFromThis);
			}

			return new List<int>();
		}

		private static IList<int> GetParentsBelowStackBased(TreeElement searchFromThis)
		{
			Stack<TreeElement> stack = new Stack<TreeElement>();
			stack.Push(searchFromThis);

			var parentsBelow = new List<int>();
			while (stack.Count > 0)
			{
				TreeElement current = stack.Pop();
				if (current.HasChildren)
				{
					parentsBelow.Add(current.Id);
					foreach (var T in current.Children)
					{
						stack.Push(T);
					}
				}
			}

			return parentsBelow;
		}
	}
}
