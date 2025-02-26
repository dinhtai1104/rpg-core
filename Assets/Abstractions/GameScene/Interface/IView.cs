using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assets.Abstractions.GameScene.Interface
{
    public interface IView
    {
        IViewModel ViewModel { get; }
        int Order { get; set; }
        bool IsVisible { get; set; }
        CancellationToken CancellationToken { get; }
        void Initialize();
        UniTask PostInit(IViewModel modal);
        UniTask Show();
        UniTask Hide();
        void HideNow();
        void OnUpdate();
    }
}
