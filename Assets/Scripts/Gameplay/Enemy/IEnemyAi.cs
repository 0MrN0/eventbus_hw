using Gameplay.Model;

namespace Gameplay.Enemy
{
    public interface IEnemyAi
    {
        Entity ChooseTarget();
    }
}