using UnityEngine;

namespace ScriptsForGameObjects.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject car;

        private Vector3 offSet;
        private float startYInCar;

        public void Start()
        {
            startYInCar = car.transform.position.y;
            offSet = transform.position - car.transform.position;
        }

        // Можно выполнять эти действия в Update.
        public void LateUpdate() 
        {
            var pos = new Vector3
            {
                z = car.transform.position.z + offSet.z,
                y = car.transform.position.y / 2 + startYInCar,
                x = car.transform.position.x / 2
            };
            transform.position = pos;
        }
    }
}