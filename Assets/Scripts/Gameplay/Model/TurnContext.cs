using Gameplay.Data;
using Gameplay.Extensions;

namespace Gameplay.Model
{
    public sealed class TurnContext : IReadOnlyTurnContext
    {
        public TeamColor PlayerTeam { get; private set; }
        public TeamColor ActiveTeam { get; private set; }
        public TeamColor OppositeTeam => ActiveTeam.Opposite();
        public bool IsPlayerTurn => ActiveTeam == PlayerTeam;

        public Entity Attacker { get; private set; }
        public Entity Target { get; private set; }
        public bool IsSkipped { get; private set; }

        public void StartGame(TeamColor playerTeam)
        {
            PlayerTeam = playerTeam;
            ActiveTeam = playerTeam;
        }

        public void BeginTurn(Entity attacker)
        {
            Attacker = attacker;
            Target = null;
            IsSkipped = false;
        }

        public void Skip() => IsSkipped = true;

        public void SetTarget(Entity target) => Target = target;

        public void PassTurn() => ActiveTeam = OppositeTeam;
    }
}