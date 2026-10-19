using Gameplay.Data;
using Gameplay.Extensions;

namespace Gameplay.Model
{
    public sealed class TurnContext
    {
        public TeamColor PlayerTeam = TeamColor.Blue;
        public TeamColor ActiveTeam;
        public TeamColor OppositeTeam => ActiveTeam.Opposite();

        public bool IsPlayerTurn => ActiveTeam == PlayerTeam;

        public Entity Attacker;
        public Entity Target;
        public bool SkipTurn;
    }

}