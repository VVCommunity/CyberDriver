using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VV.Core;

public class CloudSystem : VVBehaviour
{
    [SerializeField]
    private Transform car;
    private void FixedUpdate()
    {
        m_transform.position = new Vector3(m_transform.position.x, m_transform.position.y, car.position.z);
    }
}
