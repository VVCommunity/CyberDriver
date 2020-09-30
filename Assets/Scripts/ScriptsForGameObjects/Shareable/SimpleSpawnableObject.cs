using Core.Abstractions;
using Tools.Common;
using UnityEngine;

namespace ScriptsForGameObjects.Shareable
{
    public class SimpleSpawnableObject : MonoBehaviour, ISpawnable
    {
        private Vector3 position;

        public float Depth => 0;

        private new Cached<Transform> transform;

        private void Awake()
        {
            transform = new Cached<Transform>(gameObject);
            position = transform.Value.position;
        }

        public GameObject Spawn(float? z = null)
        {
            transform.Value.position = position;
            return gameObject;
        }

        public void Unspawn()
        {
        }
    }
}
