using System.Collections.Generic;
using Gameplay.Model;
using UnityEngine;

namespace Gameplay.Random
{
    public sealed class RandomService : IRandomService
    {
        public IReadOnlyList<Entity> PickRandom(List<Entity> from, int count)
        {
            var pool = new List<Entity>(from);
            var result = new List<Entity>(Mathf.Min(count, pool.Count));
            while (result.Count < count && pool.Count > 0)
            {
                var i = UnityEngine.Random.Range(0, pool.Count);
                result.Add(pool[i]);
                pool.RemoveAt(i);
            }
            return result;
        }

        public bool Roll(float probability)
        {
            return UnityEngine.Random.value < probability;
        }
    }
}