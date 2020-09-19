using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnMoveCar();
public class Car : MonoBehaviour
{
    [SerializeField]
    private Vector3 speedForward;
    [SerializeField]
    private MoveMethod methodMove;
    private Rigidbody rigidbody3D;

    private void Start()
    {
        rigidbody3D = GetComponent<Rigidbody>();
    }

    protected void FixedUpdate()
    {
        transform.Translate(speedForward * Time.fixedDeltaTime);
        methodMove.Move(transform, rigidbody3D, this);
    }
}