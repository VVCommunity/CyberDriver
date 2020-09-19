using UnityEngine;

namespace ScriptsForGameObjects.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject car;

        private Vector3 offSet;

        private void Awake()
        {
            offSet = transform.position - car.transform.position;
        }

        private void FixedUpdate()
        {
            transform.position = offSet + car.transform.position;
        }
    }
}