using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.GameScene.Interface
{
    public class BaseViewModel : IViewModel
    {
        public string Addressable { set; get; }
        public float MinTimeLoading { set; get; } = 3;
    }
}
