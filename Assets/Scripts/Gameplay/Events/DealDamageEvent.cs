using Gameplay.Model;

namespace Gameplay.Events
{
    public sealed class DealDamageEvent
    {
        public int Damage { get; private set; }
        public Entity Attacker { get; private set; }
        public Entity Target { get; private set; }

        public DealDamageEvent(int damage, Entity target, Entity attacker)
        {
            Damage = damage;
            Target = target;
            Attacker = attacker;
        }
    }
}