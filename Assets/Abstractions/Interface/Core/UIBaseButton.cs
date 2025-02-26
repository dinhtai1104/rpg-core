using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Abstractions.Interface.Core
{
    public class UIBaseButton : MonoBehaviour
    {
        private Button _button;
        private UnityAction _action;
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClicked);
        }

        public void AddListener(UnityAction action)
        {
            _action += action;
        }
        public void RemoveListener(UnityAction action)
        {
            _action -= action;
        }

        public virtual void OnClicked()
        {
            _action?.Invoke();
        }
    }
}
