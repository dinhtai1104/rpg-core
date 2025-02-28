using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.RPG.Units.Engine.Graphics
{
    public interface IGraphicEngine : IEngine
    {
        CharacterActor Owner { get; }
        void Init(CharacterActor actor);
        void SetActiveRenderer(bool active);
        void SetGraphicAlpha(float a);
        void SetFlashAmount(float amount);
        void FlashColor(float flickerDuration, int flickerAmount);
        void ClearFlashColor();
    }
}
