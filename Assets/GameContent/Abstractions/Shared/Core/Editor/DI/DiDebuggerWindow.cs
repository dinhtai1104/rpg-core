using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Abstractions.Shared.Core.DI.Editors
{
	public class DiDebuggerWindow : EditorWindow
	{
		private const string ContainerIcon = "PreMatCylinder";
		private const string ResolverIcon = "d_NetworkAnimator Icon";
		private const string InstanceIcon = "d_Prefab Icon";

		[NonSerialized] private bool _isInitialized;
		[SerializeField] private TreeViewState _treeViewState;
		[SerializeField] private MultiColumnHeaderState _multiColumnHeaderState;

		private int _id = -1;
		private SearchField _searchField;
		private Vector2 _bindingStackTraceScrollPosition;
		private IArchitecture _architecture;

		private MultiColumnTreeView TreeView { get; set; }
		private Rect SearchBarRect => new Rect(20f, 10f, position.width - 40f, 20f);
		private Rect MultiColumnTreeViewRect => new Rect(20, 30, position.width - 40, position.height - 200);

		private void OnEnable()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
			SceneManager.sceneUnloaded += OnSceneUnloaded;
			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private void OnDisable()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			SceneManager.sceneUnloaded -= OnSceneUnloaded;
			EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
		}

		private async void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			await Task.Yield();
			Refresh();
		}

		private async void OnSceneUnloaded(Scene scene)
		{
			await Task.Yield();
			Refresh();
		}

		private async void OnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
		{
			await Task.Yield();
			Refresh();
		}

		private void InitIfNeeded()
		{
			if (!_isInitialized)
			{
				// Check if it already exists (deserialized from window layout file or scriptable object)
				if (_treeViewState == null)
					_treeViewState = new TreeViewState();

				bool firstInit = _multiColumnHeaderState == null;
				var headerState = MultiColumnTreeView.CreateDefaultMultiColumnHeaderState();
				if (MultiColumnHeaderState.CanOverwriteSerializedFields(_multiColumnHeaderState, headerState))
					MultiColumnHeaderState.OverwriteSerializedFields(_multiColumnHeaderState, headerState);
				_multiColumnHeaderState = headerState;

				var multiColumnHeader = new MultiColumnHeader(headerState)
				{
					canSort = false,
					height = MultiColumnHeader.DefaultGUI.minimumHeight
				};

				if (firstInit)
					multiColumnHeader.ResizeToFit();

				var treeModel = new TreeModel<MyTreeElement>(GetData());

				TreeView = new MultiColumnTreeView(_treeViewState, multiColumnHeader, treeModel);
				TreeView.SetExpanded(treeModel.Root.Id, true);

				_searchField = new SearchField();
				_searchField.downOrUpArrowKeyPressed += TreeView.SetFocusAndEnsureSelectedItem;

				_isInitialized = true;
			}
		}

		private static List<(IResolver, Type)> BuildMatrix(Injector injector)
		{
			var resolvers = injector.ResolversByContract.Values
				.Select(r => r)
				.ToHashSet();

			return resolvers.Select(resolver => (resolver, GetContract(resolver, injector))).ToList();
		}

		private static Type GetContract(IResolver resolver, Injector injector)
		{
			foreach (var pair in injector.ResolversByContract)
			{
				if (pair.Value == resolver)
				{
					return pair.Key;
				}
			}

			return null;
		}

		private void BuildDataRecursively(MyTreeElement parent, Injector injector)
		{
			if (injector == null)
			{
				return;
			}

			var containerTreeElement = new MyTreeElement("Dependency Container", parent.Depth + 1, ++_id, ContainerIcon, () => string.Empty,
				Array.Empty<string>(),
				string.Empty, injector.GetDebugProperties().BuildCallsite, kind: string.Empty);
			parent.Children.Add(containerTreeElement);
			containerTreeElement.Parent = parent;

			foreach (var pair in BuildMatrix(injector))
			{
				var resolverTreeElement = new MyTreeElement(
					string.Join(", ", pair.Item2.GetName()), // In this case Name is not used for rendering, but for searching
					containerTreeElement.Depth + 1,
					++_id,
					ResolverIcon,
					() => pair.Item1.GetDebugProperties().Resolutions.ToString(),
					new[] { pair.Item2.GetName() },
					pair.Item1.Lifetime.ToString(),
					pair.Item1.GetDebugProperties().BindingCallsite,
					kind: pair.Item1.GetType().Name.Replace("Singleton", string.Empty).Replace("Transient", string.Empty).Replace("Resolver", string.Empty)
				);

				foreach (var (instance, callsite) in pair.Item1.GetDebugProperties().Instances)
				{
					var instanceTreeElement = new MyTreeElement(
						instance.GetType().GetName(),
						resolverTreeElement.Depth + 1,
						++_id,
						InstanceIcon,
						() => string.Empty,
						Array.Empty<string>(),
						string.Empty,
						callsite,
						string.Empty
					);

					instanceTreeElement.SetParent(resolverTreeElement);
				}

				resolverTreeElement.SetParent(containerTreeElement);
			}
		}

		private IList<MyTreeElement> GetData()
		{
			var root = new MyTreeElement("Root", -1, ++_id, ContainerIcon, () => string.Empty, Array.Empty<string>(), string.Empty, null, string.Empty);
			if (_architecture != null)
			{
				BuildDataRecursively(root, (Injector)_architecture.Injector);
			}

			var list = new List<MyTreeElement>();
			TreeElementUtility.TreeToList(root, list);
			return list;
		}

		private void OnGUI()
		{
			if (!Application.isPlaying)
			{
				return;
			}

			if (_architecture == null)
			{
				_isInitialized = false;
				_architecture = FindObjectsOfType<MonoBehaviour>().OfType<IArchitecture>().FirstOrDefault();
				return;
			}

			Repaint();
			InitIfNeeded();
			PresentDebuggerEnabled();
			GUILayout.FlexibleSpace();
			PresentStatusBar();
		}

		private void Refresh(PlayModeStateChange _ = default)
		{
			_isInitialized = false;
			InitIfNeeded();
		}

		private void SearchBar(Rect rect)
		{
			TreeView.searchString = _searchField.OnGUI(rect, TreeView.searchString);
			GUILayoutUtility.GetRect(rect.width, rect.height);
		}

		private void DoTreeView(Rect rect)
		{
			TreeView.OnGUI(rect);
			GUILayoutUtility.GetRect(rect.width, rect.height);
		}

		private void PresentDebuggerEnabled()
		{
			SearchBar(SearchBarRect);
			DoTreeView(MultiColumnTreeViewRect);

			GUILayout.Space(16);

			using (new GUILayout.HorizontalScope())
			{
				GUILayout.Space(16);

				using (new GUILayout.VerticalScope())
				{
					_bindingStackTraceScrollPosition = GUILayout.BeginScrollView(_bindingStackTraceScrollPosition);

					PresentCallSite();

					GUILayout.EndScrollView();
					GUILayout.Space(16);
				}

				GUILayout.Space(16);
			}
		}

		private void PresentStatusBar()
		{
			using (new EditorGUILayout.HorizontalScope(Styles.AppToolbar))
			{
				GUILayout.FlexibleSpace();

				var refreshIcon = EditorGUIUtility.IconContent("d_TreeEditor.Refresh");
				refreshIcon.tooltip = "Forces Tree View to Refresh";

				if (GUILayout.Button(refreshIcon, Styles.StatusBarIcon, GUILayout.Width(25)))
				{
					Refresh();
				}
			}
		}

		private void PresentCallSite()
		{
			var selection = TreeView.GetSelection();

			if (selection == null || selection.Count == 0)
			{
				return;
			}

			var item = TreeView.Find(selection.Single());

			if (item == null || item.Callsite == null)
			{
				return;
			}

			foreach (var callSite in item.Callsite)
			{
				PresentStackFrame(callSite.ClassName, callSite.FunctionName, callSite.Path, callSite.Line);
			}
		}

		private static void PresentStackFrame(string className, string functionName, string path, int line)
		{
			using (new GUILayout.HorizontalScope())
			{
				GUILayout.Label($"{className}:{functionName}()  →", Styles.StackTrace);

				if (PresentLinkButton($"{path}:{line}"))
				{
					UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(path, line);
				}
			}
		}

		private static bool PresentLinkButton(string label, params GUILayoutOption[] options)
		{
			var position = GUILayoutUtility.GetRect(new GUIContent(label), Styles.Hyperlink, options);
			position.y -= 3;
			Handles.color = Styles.Hyperlink.normal.textColor;
			Handles.DrawLine(new Vector3(position.xMin + (float)EditorStyles.linkLabel.padding.left, position.yMax),
				new Vector3(position.xMax - (float)EditorStyles.linkLabel.padding.right, position.yMax));
			Handles.color = Color.white;
			EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);
			return GUI.Button(position, label, Styles.Hyperlink);
		}
	}
}
