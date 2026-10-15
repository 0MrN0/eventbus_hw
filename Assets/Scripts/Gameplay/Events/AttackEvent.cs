using Gameplay.Model;

namespace Gameplay.Events
{
    public sealed class AttackEvent
    {
        public Entity Attacker { get; private set; }
        public Entity Target { get; private set; }

        public AttackEvent(Entity attacker, Entity target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}