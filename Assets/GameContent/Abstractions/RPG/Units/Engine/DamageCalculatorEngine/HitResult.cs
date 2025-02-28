using Assets.Abstractions.RPG.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.RPG.Units.Engine.DamageCalculatorEngine
{
    public struct HitResult
    {
        public bool Success { get; }
        public bool Immune { get; }
        public bool Critical { get; }
        public bool LastHit { get; }
        public bool Hurt { get; }
        public bool Evade { get; }
        public bool Block { get; }
        public float Damage { get; }
        public EDamageType DamageType { get; }

        public static readonly HitResult FailedResult =
            new HitResult(false, false, false, false, false, false, false, 0f, EDamageType.Physical);

        public static readonly HitResult InvincibleHitResult =
            new HitResult(false, true, false, false, false, false, false, 0f, EDamageType.Physical);

        public HitResult(bool success, bool immune, bool critical, bool lastHit, bool hurt, bool evade, bool block, float damage, EDamageType damageType)
        {
            Success = success;
            Immune = immune;
            Evade = evade;
            Block = block;
            Critical = critical;
            LastHit = lastHit;
            Hurt = hurt;
            Damage = damage;
            DamageType = damageType;
        }

        public override string ToString()
        {
            return
                $"{nameof(Success)}: {Success}, {nameof(Immune)}: {Immune}, {nameof(Critical)}: {Critical}, {nameof(LastHit)}: {LastHit}, {nameof(Hurt)}: {Hurt}, {nameof(Evade)}: {Evade}, {nameof(Block)}: {Block}, {nameof(Damage)}: {Damage}, {nameof(DamageType)}: {DamageType}";
        }
    }
}
