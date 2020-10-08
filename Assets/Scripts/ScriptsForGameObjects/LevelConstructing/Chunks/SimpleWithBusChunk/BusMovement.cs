using Tools.Common;
using UnityEngine;

namespace ScriptsForGameObjects.LevelConstructing.Chunks.SimpleWithBusChunk
{
    public class BusMovement : MonoBehaviour
    {
        [SerializeField]
        private float minBusSpeed;
        [SerializeField]
        private float maxBusSpeed;
        [SerializeField]
        private float maxXOffset;

        private float busSpeed;

        private new Cached<Transform> transform;

        public void Awake()
        {
            transform = new Cached<Transform>(gameObject);
            busSpeed = Random.Range(minBusSpeed, maxBusSpeed);
            transform.Value.Translate(-Random.Range(0, maxXOffset), 0, 0);
        }

        public void FixedUpdate()
        {
            transform.Value.Translate(0, 0, busSpeed * Time.fixedDeltaTime);
        }
    }
}
