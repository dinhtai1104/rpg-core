using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.RPG.Units.Engine.Graphics
{
    public class NullGraphicEngine : BaseNullEngine, IGraphicEngine
    {
        public CharacterActor Owner { get; private set; }

        public void Init(CharacterActor actor)
        {
            Owner = actor;
        }

        public void SetActiveRenderer(bool active)
        {
        }

        public void SetGraphicAlpha(float a)
        {
        }

        public void SetFlashAmount(float amount)
        {
        }

        public void FlashColor(float flickerDuration, int flickerAmount)
        {
        }

        public void ClearFlashColor()
        {
        }
    }
}
