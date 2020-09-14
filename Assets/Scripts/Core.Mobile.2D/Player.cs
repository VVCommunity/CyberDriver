using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VV.Core;

namespace VV.Core.Mobile2D
{
    public class Player : PlayerCore
    {
        [SerializeField]
        protected MoveMethod methodMove; 
        protected virtual void FixedUpdate()
        {
            if (isMove)
            {
                methodMove.Move(m_transform);
            }
        }
    }

}