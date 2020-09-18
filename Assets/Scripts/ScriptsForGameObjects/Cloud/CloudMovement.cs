using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField]
    private Transform car;
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, car.position.z);
    }
}
