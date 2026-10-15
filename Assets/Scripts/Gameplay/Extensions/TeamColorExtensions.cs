using Gameplay.Data;

namespace Gameplay.Extensions
{
    public static class TeamColorExtensions
    {
        public static TeamColor Opposite(this TeamColor team) => team switch
        {
            TeamColor.Blue => TeamColor.Red,
            TeamColor.Red => TeamColor.Blue,
            _ => throw new System.ArgumentOutOfRangeException(nameof(team), team, null)
        };
    }
}