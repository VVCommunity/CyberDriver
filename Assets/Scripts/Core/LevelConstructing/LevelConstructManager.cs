using Core.Abstractions;
using EasyButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.LevelConstructing
{
    public class LevelConstructManager : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> objectsToSpawn;
        private static IEnumerable<ISpawnable> objectsSpawns = new List<ISpawnable>();

        private void Awake()
        {
            objectsSpawns = objectsToSpawn.Select(o => o.GetComponent<ISpawnable>()).ToList();

            if (objectsSpawns.Any(s => s is null))
            {
                throw new Exception("Object to spawn must has ISpawnable interface");
            }
        }

        [Button]
        public static void BuildLevel()
        {
            foreach (var objectToSpawn in objectsSpawns)
            {
                objectToSpawn.Spawn();
            }
        }

        [Button]
        public static void ClearLevel()
        {
            foreach (var objectToSpawn in objectsSpawns)
            {
                objectToSpawn.Unspawn();
            }
        }
    }
}
