using Core.Abstractions;
using EasyButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Core.LevelConstructing
{
    public class LevelConstructManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerCar;
        private static Transform playerCarTransform;
        private static ISpawnable playerSpawn;

        [SerializeField]
        private float forwardDistanceToBeFilledFromEditor;
        private static float forwardDistanceToBeFilled;

        [SerializeField]
        private float backwardDistanceToBeFilledFromEditor;
        private static float backwardDistanceToBeFilled;

        [SerializeField]
        private float zToBeFilledFromFromEditor;
        private static float zToBeFilledFrom;

        [SerializeField]
        private List<GameObject> otherObjectsToSpawn;
        private static IEnumerable<ISpawnable> otherObjectsSpawns = new List<ISpawnable>();

        [SerializeField]
        private GameObject chunkParentFromEditor;
        private static GameObject chunkParent;

        [SerializeField]
        private List<GameObject> chunks;
        private static IEnumerable<IChunkSpawner> chunksSpawners = new List<IChunkSpawner>();

        private readonly static IList<IChunk> spawnedChunks = new List<IChunk>();

        public void Awake()
        {
            backwardDistanceToBeFilled = backwardDistanceToBeFilledFromEditor;
            forwardDistanceToBeFilled = forwardDistanceToBeFilledFromEditor;
            zToBeFilledFrom = zToBeFilledFromFromEditor;
            playerCarTransform = playerCar.transform;

            chunkParent = chunkParentFromEditor;

            playerSpawn = playerCar.GetComponent<ISpawnable>();
            otherObjectsSpawns = otherObjectsToSpawn.Select(o => o.GetComponent<ISpawnable>()).ToList();
            chunksSpawners = chunks.Select(o => o.GetComponent<IChunkSpawner>()).ToList();

            if (otherObjectsSpawns.Any(s => s is null))
            {
                throw new Exception($"Object to spawn must has {nameof(ISpawnable)} interface");
            }
            if (chunksSpawners.Any(s => s is null))
            {
                throw new Exception($"Chunks to spawn must has {nameof(IChunkSpawner)} interface");
            }
        }

        public void Update()
        {
            if (!GameManager.IsGamePaused)
            {
                UnspawnRedundantChunks();
                SpawnChunks();
            }
        }

        [Button]
        public static void BuildLevel()
        {
            playerSpawn.Spawn();
            foreach (var objectToSpawn in otherObjectsSpawns)
            {
                objectToSpawn.Spawn();
            }

            SpawnChunks();
        }

        [Button]
        public static void ClearLevel()
        {
            playerSpawn.Unspawn();
            foreach (var objectToSpawn in otherObjectsSpawns)
            {
                objectToSpawn.Unspawn();
            }
            foreach (var chunk in spawnedChunks)
            {
                chunk.Unspawn();
            }
            spawnedChunks.Clear();
        }

        [Button]
        public static void SpawnChunks()
        {
            var playerZ = playerCarTransform.position.z;
            var chunkIndexWherePlayer = ChunkIndexWherePointZ(playerZ);
            var zToBeFilledTo = playerZ + forwardDistanceToBeFilled;
            var chunkIndexToBeFilledTo = ChunkIndexWherePointZ(zToBeFilledTo);

            float? realZToBeFilledFrom = default;
            if (chunkIndexWherePlayer == -1 && chunkIndexToBeFilledTo == -1)
            {
                realZToBeFilledFrom = math.max(zToBeFilledFrom, playerZ - backwardDistanceToBeFilled);
            }
            if (chunkIndexWherePlayer != -1 && chunkIndexToBeFilledTo == -1)
            {
                var lastSpawnedChunk = spawnedChunks.Last();
                realZToBeFilledFrom = lastSpawnedChunk.Z + lastSpawnedChunk.Depth;
            }

            while (realZToBeFilledFrom < zToBeFilledTo)
            {
                var nextChunk = GetNextChunkToSpawn();
                nextChunk.Spawn(realZToBeFilledFrom);
                spawnedChunks.Add(nextChunk);
                realZToBeFilledFrom += nextChunk.Depth;
            }
        }

        [Button]
        public static void UnspawnRedundantChunks()
        {
            var playerZ = playerCarTransform.position.z;
            var chunkIndexWherePlayer = ChunkIndexWherePointZ(playerZ - backwardDistanceToBeFilled);
            for (int i = 0; i < chunkIndexWherePlayer; i++)
            {
                spawnedChunks.First().Unspawn();
                spawnedChunks.RemoveAt(0);
            }
        }

        private static IChunk GetNextChunkToSpawn()
        {
            var nextChunkIndex = UnityEngine.Random.Range(0, chunksSpawners.Count());
            return chunksSpawners.ElementAt(nextChunkIndex).CreateNewChunkCopyUnderTheParent(chunkParent);
        }

        private static int ChunkIndexWherePointZ(float z)
        {
            for (int i = 0; i < spawnedChunks.Count(); i++)
            {
                var chunk = spawnedChunks.ElementAt(i);
                if (z >= chunk.Z && z <= chunk.Z + chunk.Depth)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
