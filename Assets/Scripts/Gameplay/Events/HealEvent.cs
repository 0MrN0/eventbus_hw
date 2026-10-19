using Gameplay.Model;

namespace Gameplay.Events
{
    public sealed class HealEvent
    {
        public Entity Target;
        public Entity Attacker;
        public int HealAmount;

        public HealEvent(Entity target, int healAmount, Entity attacker)
        {
            Target = target;
            HealAmount = healAmount;
            Attacker = attacker;
        }
    }
}