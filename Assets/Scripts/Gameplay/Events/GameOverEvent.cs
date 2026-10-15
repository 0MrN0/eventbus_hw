using Gameplay.Data;

namespace Gameplay.Events
{
    public sealed class GameOverEvent
    {
        public TeamColor? Winner { get; private set; }

        public GameOverEvent(TeamColor? winner)
        {
            Winner = winner;
        }
    }
}