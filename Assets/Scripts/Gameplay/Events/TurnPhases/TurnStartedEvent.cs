using Gameplay.Model;

namespace Gameplay.Events
{
    public sealed class TurnStartedEvent
    {
        public Entity Attacker { get; private set; }
        public bool SkipTurn { get; set; }

        public TurnStartedEvent(Entity attacker)
        {
            Attacker = attacker;
        }
    }
}