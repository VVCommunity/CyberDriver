using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Car car;
    [SerializeField]
    private Vector3 offSet;

    private void FixedUpdate()
    {
        transform.position = offSet + car.transform.position;
    }
}
