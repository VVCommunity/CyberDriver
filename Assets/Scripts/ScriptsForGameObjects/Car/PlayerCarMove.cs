using Core;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace ScriptsForGameObjects.Car
{
    public class PlayerCarMove : MonoBehaviour
    {
        [SerializeField]
        private float maxPlaneSpeed = 20f;
        [SerializeField]
        private float maxSpeedTouchLength = 200f;
        [SerializeField]
        private float forwardSpeed = 20f;
        [SerializeField]
        private float accelerationFactor = 0.5f;
        [SerializeField]
        private float gapBetweenAccelerations = 6f;
        [SerializeField]
        private Transform body;

        public float ForwardSpeed
        {
            get => forwardSpeed;
            set
            {
                forwardSpeed = value;
                if (forwardSpeed < 0)
                {
                    forwardSpeed = 0;
                }
            }
        }

        private float approximationFactor;

        new private Rigidbody rigidbody;

        private Vector2 targetPlanePosition;
        private Vector2 touchStartPosition;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            approximationFactor = 90f / (GameManager.DistanceBetweenWalls / 2);
            targetPlanePosition = new Vector2(transform.position.x, transform.position.y);
            StartCoroutine(IncreaseSpeed());
        }

        private void FixedUpdate()
        {
            var x = transform.position.x;
            body.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, approximationFactor * x));
            // Define forward target position.
            var targetZPosition = transform.position.z + (ForwardSpeed * Time.fixedDeltaTime);
            // Define plane target position.
            if (Input.touchCount > 0)
            {
                var touch = Input.touches.First();
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchStartPosition = touch.position;
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        var coercedDeltaTouch = (touch.position - touchStartPosition) / maxSpeedTouchLength;
                        if (coercedDeltaTouch.magnitude > 1)
                        {
                            coercedDeltaTouch.Normalize();
                        }

                        // Checking for contact with the left wall.
                        if (transform.position.x >= GameManager.DistanceBetweenWalls / 2 && coercedDeltaTouch.x > 0)
                        {
                            coercedDeltaTouch.x = 0;
                        }
                        // Checking for contact with the right wall.
                        else if (transform.position.x <= -GameManager.DistanceBetweenWalls / 2 && coercedDeltaTouch.x < 0)
                        {
                            coercedDeltaTouch.x = 0;
                        }

                        // Checking for contact with the upper bound.
                        if (transform.position.y >= GameManager.MaxHeight && coercedDeltaTouch.y > 0)
                        {
                            coercedDeltaTouch.y = 0;
                        }
                        // Checking for contact with the lower bound.
                        else if (transform.position.y <= GameManager.MinHeight && coercedDeltaTouch.y < 0)
                        {
                            coercedDeltaTouch.y = 0;
                        }

                        targetPlanePosition += (maxPlaneSpeed * Time.fixedDeltaTime) * coercedDeltaTouch;
                        break;
                }
            }
            // Move to target position.
            rigidbody.MovePosition(new Vector3(targetPlanePosition.x, targetPlanePosition.y, targetZPosition));
        }

        private IEnumerator IncreaseSpeed()
        {
            while (true)
            {
                ForwardSpeed += accelerationFactor;
                yield return new WaitForSeconds(gapBetweenAccelerations);
            }
        }
    }
}