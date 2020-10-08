using System;
using UnityEngine;

namespace ScriptsForGameObjects.Car
{
    public class PlayerCarCollision : MonoBehaviour
    {
        [SerializeField]
        private PlayerCarMove playerCarMoveScript;
        [SerializeField]
        private float deceleration = 5f;

        private void OnTriggerEnter(Collider other)
        {
            var tag = other.gameObject.tag;
            if (tag.Equals("obstacle", StringComparison.InvariantCultureIgnoreCase))
            {
                playerCarMoveScript.ForwardSpeed -= deceleration;
                Destroy(other.gameObject);
                return;
            }
            if (tag.Equals("cargo", StringComparison.InvariantCultureIgnoreCase))
            {
                // Add one cargo to the cargo counter in the car manager
                // var cargo = other.gameObject;
                // cargo.GetComponent<ICargo>().Condition = CargoCondition.Caught;
                // cargo.GetComponent<Rigidbody>().isKinematic = true;
                // cargo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                // cargo.transform.parent = transform.parent;
                // for next task...
                return;
            }
            if (tag.Equals("police", StringComparison.InvariantCultureIgnoreCase))
            {
                Debug.Log("GAME OVER!");
                return;
            }
        }
    }
}