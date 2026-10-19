using Gameplay.Model;

namespace Gameplay.Events
{
    public sealed class BackStrikeEvent
    {
        public Entity Attacker { get; private set; }
        public Entity Target { get; private set; }

        public BackStrikeEvent(Entity attacker, Entity target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}