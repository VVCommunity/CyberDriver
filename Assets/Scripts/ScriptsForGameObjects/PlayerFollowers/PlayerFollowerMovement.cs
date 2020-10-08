using Tools.Common;
using UnityEngine;

namespace ScriptsForGameObjects.Camera
{
    public class PlayerFollowerMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        private Cached<Transform> playerTransform;

        private new Cached<Transform> transform;

        private Vector3 offSet;

        public void Awake()
        {
            transform = new Cached<Transform>(gameObject);
            playerTransform = new Cached<Transform>(player);
            offSet = transform.Value.position - playerTransform.Value.position;
        }

        public void FixedUpdate()
        {
            transform.Value.position = offSet + playerTransform.Value.position;
        }
    }
}