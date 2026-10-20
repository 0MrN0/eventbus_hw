using Gameplay.Model;

namespace Gameplay.Events
{
    public sealed class BeforeDamageEvent
    {
        public Entity Attacker { get; private set; }
        public Entity Target { get; private set; }
        public int Damage { get; set; }

        public BeforeDamageEvent(int damage, Entity target, Entity attacker)
        {
            Attacker = attacker;
            Target = target;
            Damage = damage;
        }
    }
}