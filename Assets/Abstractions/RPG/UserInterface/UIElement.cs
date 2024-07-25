using UnityEngine;

namespace Assets.Abstractions.RPG.UserInterface
{
    public class UIElement : MonoBehaviour
    {
        private RectTransform _rectTransform;
        public RectTransform RectTrans
        {
            get
            {
                if (_rectTransform == null) _rectTransform = transform as RectTransform;
                return _rectTransform;
            }
        }

        public bool Active
        {
            set
            {
                if (gameObject.activeSelf != value)
                {
                    gameObject.SetActive(value);
                }
            }
            get
            {
                return gameObject.activeSelf;
            }
        }

        protected virtual void Awake() { }
        protected virtual void OnDestroy() { }
        protected virtual void Start() { }
        public virtual void Init() { }
        public virtual void OnUpdate() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
    }
}
