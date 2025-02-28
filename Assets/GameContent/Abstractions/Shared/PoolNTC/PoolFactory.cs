using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Pool = NTC.Pool.NightPool;
namespace Abstractions.Shared
{
    public static class PoolFactory
    {
        public static T Spawn<T>(T prefab) where T : Component
        {
            if (prefab == null)
                return default;

            return Pool.Spawn(prefab);
        }

        public static T Spawn<T>(T prefab, Transform parent) where T : Component
        {
            if (prefab == null)
                return default;

            return Pool.Spawn(prefab, parent);
        }

        public static GameObject Spawn(GameObject prefab)
        {
            if (prefab == null)
                return default;

            return Pool.Spawn(prefab);
        }

        public static GameObject Spawn(GameObject prefab, Transform parent)
        {
            if (prefab == null || parent == null)
                return default;

            return Pool.Spawn(prefab, parent);
        }

        public static void Despawn<T>(T clone) where T : Component
        {
            if (clone == null)
                return;

            Despawn(clone.gameObject);
        }

        public static void Despawn(GameObject clone)
        {
            if (clone == null)
                return;

            Pool.Despawn(clone);
        }

        public static void Despawn(params GameObject[] clone)
        {
            if (clone == null)
                return;

            foreach (var item in clone)
            {
                Despawn(item);
            }
        }

        public static void Despawn<T>(params T[] clone) where T : Component
        {
            if (clone == null)
                return;

            foreach (var item in clone)
            {
                Despawn(item);
            }
        }

        public static void Despawn<T>(IEnumerable<T> clones) where T : Component
        {
            if (clones == null)
                return;

            foreach (var item in clones)
            {
                Despawn(item);
            }
        }

        public static void Despawn(IEnumerable<GameObject> clones)
        {
            if (clones == null)
                return;

            foreach (var item in clones)
            {
                Despawn(item);
            }
        }

        public static void DespawnAll()
        {
            Pool.DestroyAllPools();
        }
    }
}
