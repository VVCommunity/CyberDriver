using Core.Abstractions;
using UnityEngine;

namespace ScriptsForGameObjects.LevelConstructing
{
    public class SelfChunkSpawner : MonoBehaviour, IChunk, IChunkSpawner
    {
        [SerializeField]
        private float depth;
        public float Depth { get; private set; }

        public float Z { get; private set; }

        public void Awake()
        {
            Depth = depth;
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
                transform.position = new Vector3(0, 0, Z);
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
