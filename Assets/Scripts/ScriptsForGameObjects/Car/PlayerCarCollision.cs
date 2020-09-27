using Assets.Scripts.Core.Abstractions;
using Assets.Scripts.Core.Entities;
using ScriptsForGameObjects.Car;
using UnityEngine;

public class PlayerCarCollision : MonoBehaviour
{
    [SerializeField]
    private PlayerCarMove playerCarMoveScript;
    [SerializeField]
    private float deceleration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Obstacle")
        {
            playerCarMoveScript.ForwardSpeed -= deceleration;
            Destroy(other.gameObject);
        }
        else if (tag == "Cargo")
        {
            // Add one cargo to the cargo counter in the car manager
            // var cargo = other.gameObject;
            // cargo.GetComponent<ICargo>().Condition = CargoCondition.Caught;
            // cargo.GetComponent<Rigidbody>().isKinematic = true;
            // cargo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            // cargo.transform.parent = transform.parent;
            // for next task...
        }
    }
}
