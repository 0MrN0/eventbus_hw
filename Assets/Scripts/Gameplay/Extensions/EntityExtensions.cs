using Gameplay.Data;
using Gameplay.Model;
using UI;

namespace Gameplay.Extensions
{
    public static class EntityExtensions
    {
        public static TeamColor GetTeam(this Entity entity)
        {
            return entity.Get<TeamComponent>().Value;
        }

        public static HeroView GetView(this Entity entity)
        {
            return entity.Get<ViewComponent>().Value;
        }

        public static bool IsAlive(this Entity entity)
        {
            return entity.Get<Health>().Current > 0;
        }

        public static string GetStatsText(this Entity entity)
        {
            return $"{entity.Get<Health>().Current} / {entity.Get<AttackDamage>().Value}";
        }
    }
}
