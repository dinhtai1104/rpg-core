using Assets.Abstractions.GameScene.Core;
using Assets.Abstractions.GameScene.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.GameScene.Loading
{
    public struct LoadingModel : IViewModel
    {
        public string Addressable { set; get; }
        public float MinTimeLoading { set; get; }
    }
    public class UIPanelLoading : BaseView
    {
    }
}
