using System;
using Gameplay.Data;
using UI;

namespace Gameplay.Model
{
    public interface IComponent { }
    public interface IAbilityComponent : IComponent { }

    [Serializable]
    public struct Health : IComponent
    {
        public int Max;
        public int Current;
    }

    [Serializable]
    public struct AttackDamage : IComponent
    {
        public int Value;
    }

    [Serializable]
    public struct ExtraAttackAbility : IAbilityComponent
    {
        public int TargetsCount;
        public int Damage;
    }

    [Serializable]
    public struct HealAbility : IAbilityComponent
    {
        public int TargetsCount;
        public int Value;
    }

    [Serializable]
    public struct StunAbility : IAbilityComponent
    {
        public int Turns;
    }

    [Serializable]
    public struct HealthStealingAbility : IAbilityComponent
    {
        public float Probability;
    }

    [Serializable]
    public struct MissAbility : IAbilityComponent
    {
        public float Probability;
    }

    [Serializable]
    public struct BackStrikeImmunAbility : IAbilityComponent
    {

    }

    [Serializable]
    public struct StunStatus : IComponent
    {
        public int TurnsLeft;
    }

    [Serializable]
    public struct ImmuneStatus : IComponent
    {
        public int StrikesLeft;
    }

    [Serializable]
    public struct TeamComponent : IComponent
    {
        public TeamColor Value;
    }

    public struct ViewComponent : IComponent
    {
        public HeroView Value;
    }

    public struct NameComponent : IComponent
    {
        public string Value;
    }
}