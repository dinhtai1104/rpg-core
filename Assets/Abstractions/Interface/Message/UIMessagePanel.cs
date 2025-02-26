using Assets.Abstractions.GameScene.Core;
using Assets.Abstractions.GameScene.Interface;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Abstractions.Interface.Message
{
    public struct MessagePanelModel : IViewModel
    {
        public string Addressable { set; get; }
        public string Message { set; get; }
    }
    public class UIMessagePanel : BaseView
    {
        [SerializeField] private TextMeshProUGUI _textMessage;

        public override async UniTask PostInit(IViewModel viewModel)
        {
            await base.PostInit(viewModel);
            _textMessage.SetText(((MessagePanelModel)ViewModel).Message);
        }
    }
}
