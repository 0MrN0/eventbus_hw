using Gameplay.Data;
using UI;

namespace Gameplay.Extensions
{
    public static class UiExtensions
    {
        public static HeroListView GetListViewByTeam(this UIService ui, TeamColor team) => team switch
        {
            TeamColor.Blue => ui.GetBluePlayer(),
            TeamColor.Red => ui.GetRedPlayer(),
            _ => throw new System.ArgumentOutOfRangeException(nameof(team), team, null)
        };
    }
}