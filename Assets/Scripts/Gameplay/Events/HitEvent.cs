using Gameplay.Model;

namespace Gameplay.Events
{
    public sealed class HitEvent
    {
        public Entity Attacker { get; private set; }
        public Entity Target { get; private set; }

        public HitEvent(Entity attacker, Entity target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}