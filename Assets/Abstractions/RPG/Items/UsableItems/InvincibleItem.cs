using Abstractions.Shared.MEC;
using Assets.Abstractions.RPG.Units;
using Assets.Abstractions.RPG.Units.Engine.Healths;
using System.Collections.Generic;

namespace Assets.Abstractions.RPG.Items.UsableItems
{
    public class InvincibleItem : UseableItem
    {
        private float _timeInvincible;

        public override void Use(ICharacter character)
        {
            if (character.GetEngine<IHealth>().Invincible) return;
            base.Use(character);
            character.GetEngine<IHealth>().Invincible = true;
            Timing.RunCoroutine(InvincibleTime(character));
        }

        private IEnumerator<float> InvincibleTime(ICharacter character)
        {
            yield return Timing.WaitForSeconds(_timeInvincible);
            character.GetEngine<IHealth>().Invincible = false;
        }
    }
}
