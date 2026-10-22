using Gameplay.Data;

namespace Gameplay.Model
{
    public interface IReadOnlyTurnContext
    {
        TeamColor PlayerTeam { get; }
        TeamColor ActiveTeam { get; }
        TeamColor OppositeTeam { get; }
        bool IsPlayerTurn { get; }

        Entity Attacker { get; }
        Entity Target { get; }
        bool IsSkipped { get; }
    }
}