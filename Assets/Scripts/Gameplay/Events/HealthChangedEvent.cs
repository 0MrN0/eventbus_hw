using Gameplay.Model;

namespace Gameplay.Events
{
    public sealed class HealthChangedEvent
    {
        public Entity Target { get; private set; }
        public int HealthBefore { get; private set; }
        public int HealthAfter { get; private set; }

        public HealthChangedEvent(Entity target, int healthBefore, int healthAfter)
        {
            Target = target;
            HealthBefore = healthBefore;
            HealthAfter = healthAfter;
        }
    }
}
