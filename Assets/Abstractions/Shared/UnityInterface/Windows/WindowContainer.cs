using System;
using Assets.Abstractions.Shared.Foundation;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[RequireComponent(typeof(RectMask2D), typeof(CanvasGroup))]
	public abstract class WindowContainer : ViewContainer, IWindowContainer
	{
		public string ContainerName { get; private set; }
		public WindowContainerType ContainerType { get; private set; }
		public IUIManager UIManager { get; private set; }
		public Canvas Canvas { get; private set; }

		protected WindowContainerConfig Config { get; private set; }

		public void Initialize(IUIManager uiManager, WindowContainerConfig config, UISettings settings)
		{
			Config = config ?? throw new ArgumentNullException(nameof(config));
			Settings = settings ? settings : throw new ArgumentNullException(nameof(settings));

			UIManager = uiManager ?? throw new ArgumentNullException(nameof(uiManager));
			UIManager.AddContainer(this);

			ContainerName = config.Name;
			ContainerType = config.ContainerType;

			var canvas = GetComponent<Canvas>();

			if (config.OverrideSorting)
			{
				canvas.overrideSorting = true;
				canvas.sortingLayerID = config.SortingLayer.ID;
				canvas.sortingOrder = config.OrderInLayer;
			}

			Canvas = canvas;

			InitializePool();
			OnInitialize();
		}

		protected virtual void OnInitialize() { }

		protected override void InitializePool()
		{
			base.InitializePool();
			var poolCanvas = PoolTransform.gameObject.GetOrAddComponent<Canvas>();
			poolCanvas.overrideSorting = true;
			poolCanvas.sortingLayerID = Canvas.sortingLayerID;
			poolCanvas.sortingOrder = Canvas.sortingOrder;
		}
	}
}
