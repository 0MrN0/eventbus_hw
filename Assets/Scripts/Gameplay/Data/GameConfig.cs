using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(menuName = "Configs/Game")]
    public sealed class GameConfig : ScriptableObject
    {
        public TeamColor PlayerColor;
        [Min(0)] public int EnemyThinkDelayMs = 500;
        public HeroConfig[] Blue;
        public HeroConfig[] Red;
    }
}
