using Tools.Common;
using UnityEngine;

namespace ScriptsForGameObjects.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject car;
        private Cached<Transform> carTransform;

        private new Cached<Transform> transform;

        private Vector3 offSet;
        private float startYInCar;

        private void Awake()
        {
            carTransform = new Cached<Transform>(car);
            transform = new Cached<Transform>(gameObject);
            startYInCar = carTransform.Value.position.y;
            offSet = transform.Value.position - carTransform.Value.position;
        }

        // Можно выполнять эти действия в Update.
        public void LateUpdate()
        {
            transform.Value.position = new Vector3
            {
                z = carTransform.Value.position.z + offSet.z,
                y = carTransform.Value.position.y / 2 + startYInCar,
                x = carTransform.Value.position.x / 2,
            };
        }
    }
}