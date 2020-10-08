using Core;
using System.Collections;
using Tools.Common;
using UnityEngine;

namespace ScriptsForGameObjects.Police
{
    public class PoliceMove : MonoBehaviour
    {
        [SerializeField]
        private float forwardSpeed = 17.5f;
        [SerializeField]
        private float accelerationFactor = 0.25f;
        [SerializeField]
        private float gapBetweenAccelerations = 6f;
        [SerializeField]
        private Transform target;
        [SerializeField]
        private Transform body;

        private float approximationFactor;

        private new Cached<Rigidbody> rigidbody;
        private new Cached<Transform> transform;

        private void Awake()
        {
            rigidbody = new Cached<Rigidbody>(gameObject);
            transform = new Cached<Transform>(gameObject);
        }

        private void Start()
        {
            approximationFactor = 90f / (GameManager.DistanceBetweenWalls / 2);
            StartCoroutine(IncreaseSpeed());
        }

        private void FixedUpdate()
        {
            var x = transform.Value.position.x;
            body.localRotation = Quaternion.Euler(new Vector3(0, 0, approximationFactor * x));
            var targetZPosition = transform.Value.position.z + forwardSpeed * Time.fixedDeltaTime;
            rigidbody.Value.MovePosition(new Vector3(target.position.x, target.position.y, targetZPosition));
        }

        private IEnumerator IncreaseSpeed()
        {
            while (true)
            {
                forwardSpeed += accelerationFactor;
                yield return new WaitForSeconds(gapBetweenAccelerations);
            }
        }
    }
}