using UnityEngine;

namespace ScriptsForGameObjects.Camera
{
    public class PlayerFollowerMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;

        private Vector3 offSet;

        public void Awake()
        {
            offSet = transform.position - player.transform.position;
        }

        public void FixedUpdate()
        {
            transform.position = offSet + player.transform.position;
        }
    }
}