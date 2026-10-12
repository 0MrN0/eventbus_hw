using UI;

namespace Gameplay.CoreLoop
{
    public sealed class TurnContext
    {
        public HeroListView ActivePlayer;
        public HeroListView EnemyPlayer;
        public HeroView Target;
    }
}