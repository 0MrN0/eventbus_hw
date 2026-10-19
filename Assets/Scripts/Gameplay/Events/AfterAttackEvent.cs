using Gameplay.Model;

namespace Gameplay.Events
{
    public sealed class AfterAttackEvent
    {
        public Entity Attacker { get; private set; }
        public Entity Target { get; private set; }

        public AfterAttackEvent(Entity attacker, Entity target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}