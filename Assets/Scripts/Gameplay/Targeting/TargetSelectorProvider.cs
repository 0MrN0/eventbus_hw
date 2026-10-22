using Gameplay.Data;
using Gameplay.Enemy;
using Gameplay.Input;
using Gameplay.Model;

namespace Gameplay.Targeting
{
    public sealed class TargetSelectorProvider
    {
        private readonly IReadOnlyTurnContext _turnContext;
        private readonly PlayerTargetSelector _player;
        private readonly IEnemyAi _enemy;

        public TargetSelectorProvider(IReadOnlyTurnContext turnContext, PlayerTargetSelector player, IEnemyAi enemy)
        {
            _turnContext = turnContext;
            _player = player;
            _enemy = enemy;
        }

        public ITargetSelector GetFor(TeamColor team)
        {
            return team == _turnContext.PlayerTeam ? _player : _enemy;
        }
    }
}
