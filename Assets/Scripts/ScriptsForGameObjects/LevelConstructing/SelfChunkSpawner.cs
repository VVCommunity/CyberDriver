using Core.Abstractions;
using Tools.Common;
using UnityEngine;

namespace ScriptsForGameObjects.LevelConstructing
{
    public class SelfChunkSpawner : MonoBehaviour, IChunk, IChunkSpawner
    {
        [SerializeField]
        private float depth;
        public float Depth { get; private set; }

        public float Z { get; private set; }

        private new Cached<Transform> transform;

        public void Awake()
        {
            Depth = depth;
            transform = new Cached<Transform>(gameObject);
        }

        public IChunk CreateNewChunkCopyUnderTheParent(GameObject parent)
        {
            var obj = Instantiate(gameObject, parent.transform);
            obj.SetActive(false);
            return obj.GetComponent<IChunk>();
        }

        public GameObject Spawn(float? z = null)
        {
            if (z.HasValue)
            {
                Z = z.Value;
                transform.Value.position = new Vector3(0, 0, Z);
                gameObject.SetActive(true);
                return gameObject;
            }
            return null;
        }

        public void Unspawn()
        {
            Destroy(gameObject);
        }
    }
}
