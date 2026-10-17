using System;

namespace Gameplay.Model
{
    public interface IComponentProvider
    {
        void Install(Entity hero);
    }

    [Serializable]
    public abstract class ComponentProvider<T> : IComponentProvider where T : struct, IComponent
    {
        public T Value;
        public void Install(Entity hero) => hero.Set(Value);
    }

    [Serializable]
    public abstract class TagProvider<T> : IComponentProvider where T : struct, IAbilityComponent
    {
        public void Install(Entity hero) => hero.Set(default(T));
    }

    [Serializable]
    public sealed class HealAbilityProvider : ComponentProvider<HealAbility> { }

    [Serializable]
    public sealed class StunAbilityProvider : ComponentProvider<StunAbility> { }

    [Serializable]
    public sealed class BackStrikeImmunAbilityProvider : TagProvider<BackStrikeImmunAbility> { }

    [Serializable]
    public sealed class MissAbilityProvider : ComponentProvider<MissAbility> { }

    [Serializable]
    public sealed class HealthStealingAbilityProvider : ComponentProvider<HealthStealingAbility> { }

    [Serializable]
    public sealed class ExtraAttackAbilityProvider : ComponentProvider<ExtraAttackAbility> { }

    [Serializable]
    public sealed class FirstStrikeImmuneComponentProvider : ComponentProvider<ImmuneStatus> { }
}