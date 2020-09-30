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
        if (other.gameObject.CompareTag("Obstacle"))
        {
            playerCarMoveScript.ForwardSpeed -= deceleration;
            Destroy(other.gameObject);
            return;
        }
        if (other.gameObject.CompareTag("Cargo"))
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
        if (other.gameObject.CompareTag("Police"))
        {
            Debug.Log("GAME OVER!");
            return;
        }
    }
}
