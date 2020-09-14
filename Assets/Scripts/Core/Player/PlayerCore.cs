using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VV.Core;

public class PlayerCore : VVBehaviour
{

    [SerializeField]
    protected Transform m_camera;
    protected Vector2 screenPostion
    {
        get
        {
            return Camera.main.WorldToScreenPoint(m_transform.position);
        }

    }
    [SerializeField]
    protected bool isMove = true;
    [SerializeField]
    protected bool isGod = false;
    [SerializeField]
    protected bool isDev = false;
     
}
