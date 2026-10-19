using System.Collections.Generic;
using Gameplay.Model;

namespace Gameplay.Random
{
    public interface IRandomService
    {
        IReadOnlyList<Entity> PickRandom(List<Entity> entities, int count);
        bool Roll(float probability);
    }
}