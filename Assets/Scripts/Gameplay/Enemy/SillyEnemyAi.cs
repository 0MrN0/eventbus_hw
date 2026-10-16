using Gameplay.Model;

namespace Gameplay.Enemy
{
    public sealed class SillyEnemyAi : IEnemyAi
    {
        private readonly HeroRegistry _registry;
        private readonly TurnContext _context;

        public SillyEnemyAi(HeroRegistry registry, TurnContext context)
        {
            _registry = registry;
            _context = context;
        }

        public Entity ChooseTarget()
        {
            return _registry.GetAliveTeam(_context.OppositeTeam)[0];
        }
    }
}