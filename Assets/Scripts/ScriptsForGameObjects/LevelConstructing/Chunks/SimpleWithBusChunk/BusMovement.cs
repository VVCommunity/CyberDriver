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

        public void Awake()
        {
            busSpeed = Random.Range(minBusSpeed, maxBusSpeed);
            transform.Translate(-Random.Range(0, maxXOffset), 0, 0);
        }

        public void FixedUpdate()
        {
            transform.Translate(0, 0, busSpeed * Time.fixedDeltaTime);
        }
    }
}
