using ScriptsForGameObjects.Car;
using System.Collections;
using System.Collections.Generic;
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
        }
    }
}
