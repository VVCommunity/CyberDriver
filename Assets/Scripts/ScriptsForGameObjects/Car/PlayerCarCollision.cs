using ScriptsForGameObjects.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarCollision : MonoBehaviour
{
    [SerializeField]
    private PlayerCarMove playerCarMoveScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            playerCarMoveScript.ForwardSpeed -= 7.25f;
            Destroy(other.gameObject);
        }
    }
}
