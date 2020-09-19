using Core.Abstractions;
using UnityEngine;

namespace Assets.Scripts.ScriptsForGameObjects.Shareable
{
    public class SimpleSpawnableObject : MonoBehaviour, ISpawnable
    {
        private Vector3 position;

        private void Awake()
        {
            position = transform.position;
        }

        public GameObject Spawn(float? z = null)
        {
            transform.position = position;
            return gameObject;
        }

        public void Unspawn()
        {
        }
    }
}
