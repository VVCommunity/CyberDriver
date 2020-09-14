using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VV.Core;

public class CameraController : VVBehaviour
{
    public Car car;
    public Vector3 offSet;
   
    private void FixedUpdate()
    {
        m_transform.position = offSet + car.GetTransform.position;
        m_transform.LookAt(car.m_centerMass);
    }
}
