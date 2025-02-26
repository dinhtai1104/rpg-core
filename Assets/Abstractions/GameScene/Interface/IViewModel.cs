using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.GameScene.Interface
{
    public interface IViewModel
    {
        string Addressable { set; get; }
        float MinTimeLoading { set; get; }
    }
}
