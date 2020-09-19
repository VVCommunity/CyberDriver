using UnityEngine;

namespace ScriptsForGameObjects
{
    public class PlayerCar : MonoBehaviour
    {
        [SerializeField]
        private Vector3 speedForward;
        [SerializeField]
        private MoveMethod movingMethod;

        new private Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            transform.Translate(speedForward * Time.fixedDeltaTime);
            movingMethod.Move(transform, rigidbody, this);
        }
    }
}