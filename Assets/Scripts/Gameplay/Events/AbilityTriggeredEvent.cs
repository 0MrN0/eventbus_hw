using Gameplay.Model;

namespace Gameplay.Events
{
    public sealed class AbilityTriggeredEvent
    {
        public Entity Owner { get; private set; }

        public AbilityTriggeredEvent(Entity owner)
        {
            Owner = owner;
        }
    }
}
