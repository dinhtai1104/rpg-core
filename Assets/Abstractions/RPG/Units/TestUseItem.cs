using Assets.Abstractions.RPG.Items.UsableItems;
using Assets.Abstractions.RPG.Value;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Abstractions.RPG.Units
{
    public class TestUseItem : MonoBehaviour
    {
        [SerializeField] private CharacterActor actor;

        [Button]
        public void UseHealPoint()
        {
            var healpoint = new HealthPoisonItem(1, 10);
            healpoint.Use(actor);
        }
    }
}
