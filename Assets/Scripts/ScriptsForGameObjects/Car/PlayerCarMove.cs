using Core;
using System.Linq;
using Tools.Common;
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
        private Transform body;

        [SerializeField]
        public float acceleration = 0.1f;

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

        private new Cached<Rigidbody> rigidbody;
        private new Cached<Transform> transform;

        private Vector2 targetPlanePosition;
        private Vector2 touchStartPosition;

        private void Awake()
        {
            rigidbody = new Cached<Rigidbody>(gameObject);
            transform = new Cached<Transform>(gameObject);
        }

        private void Start()
        {
            approximationFactor = 90f / (GameManager.DistanceBetweenWalls / 2);
            targetPlanePosition = new Vector2(transform.Value.position.x, transform.Value.position.y);
        }

        private void FixedUpdate()
        {
            var x = transform.Value.position.x;
            body.localRotation = Quaternion.Euler(new Vector3(0, 0, approximationFactor * x));
            // Define forward target position.
            var targetZPosition = transform.Value.position.z + (ForwardSpeed * Time.fixedDeltaTime);
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
                        if (transform.Value.position.x >= GameManager.DistanceBetweenWalls / 2 && coercedDeltaTouch.x > 0)
                        {
                            coercedDeltaTouch.x = 0;
                        }
                        // Checking for contact with the right wall.
                        else if (transform.Value.position.x <= -GameManager.DistanceBetweenWalls / 2 && coercedDeltaTouch.x < 0)
                        {
                            coercedDeltaTouch.x = 0;
                        }

                        // Checking for contact with the upper bound.
                        if (transform.Value.position.y >= GameManager.MaxHeight && coercedDeltaTouch.y > 0)
                        {
                            coercedDeltaTouch.y = 0;
                        }
                        // Checking for contact with the lower bound.
                        else if (transform.Value.position.y <= GameManager.MinHeight && coercedDeltaTouch.y < 0)
                        {
                            coercedDeltaTouch.y = 0;
                        }

                        targetPlanePosition += maxPlaneSpeed * Time.fixedDeltaTime * coercedDeltaTouch;
                        break;
                }
            }

            // Move to target position.
            rigidbody.Value.MovePosition(new Vector3(targetPlanePosition.x, targetPlanePosition.y, targetZPosition));

            // Speed up.
            ForwardSpeed += acceleration * Time.fixedDeltaTime;
        }
    }
}