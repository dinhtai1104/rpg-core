using Assets.Abstractions.RPG.Items;
using Assets.Abstractions.RPG.Manager;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Abstractions.RPG.UserInterface.Items
{
    public abstract class UIBaseItem : UIElement
    {
        private BaseRuntimeItem runtimeItem;
        public virtual BaseRuntimeItem RuntimeItem
        {
            get => runtimeItem;
            set => runtimeItem = value;
        }
        protected IResourceManager ResourceManager { get => resourceManager; set => resourceManager = value; }

        private IResourceManager resourceManager;
        protected UISlotItem _slotItem;

        [SerializeField] private Image _iconItem;
        [SerializeField] private Image _frameItem;

        protected override void Awake()
        {
            base.Awake();
            ResourceManager = Game.Instance.GetService<IResourceManager>();
        }

        public void SetData(BaseRuntimeItem runtimeItem, UISlotItem uiSlot)
        {
            this.runtimeItem = runtimeItem;
            this._slotItem = uiSlot;
            uiSlot.SetAmount(0); // always zero to hide this

            // SetIcon
            var icon = ResourceManager.Get<Sprite>($"Icon/{RuntimeItem.PathUIIcon}");
            SetIcon(icon);

            // SetIcon
            var framebase = ResourceManager.Get<Sprite>($"Icon/{ERarity.Uncommon}");
            SetFrame(framebase);

            SetData();
        }
        public void SetIcon(Sprite icon, bool setNative = false)
        {
            _iconItem.sprite = icon;
            if (setNative)
            {
                _iconItem.SetNativeSize();
            }
        }
        public void SetFrame(Sprite frame)
        {
            _frameItem.sprite = frame;
        }
        protected abstract void SetData();
    }

    public abstract class UIBaseItem<TRuntimeItem> : UIBaseItem where TRuntimeItem : BaseRuntimeItem
    {
        protected new TRuntimeItem RuntimeItem => (TRuntimeItem)base.RuntimeItem;
    }
}
