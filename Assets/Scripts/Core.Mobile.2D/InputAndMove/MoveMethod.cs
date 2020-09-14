using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems; 


[CreateAssetMenu(fileName ="Character", menuName = "Characters/Standart")]
public class MoveMethod : ScriptableObject
{ 
    public float speed;

    [Range(0, 20)]
    public float speedEnd = 1;

    
  
    public virtual void Enable(Transform m_transform)
    {
    //    width = (float)Screen.width / 2.0f;
       // height = (float)Screen.height / 2.0f;  
    }
    
    public virtual void Move(Transform m_transform, Rigidbody2D rigidbody)
    {
        // The screen has been touched so store the touch

    }
    public virtual void Move(Transform m_transform, Rigidbody  rigidbody)
    {
        // The screen has been touched so store the touch

    }
    public virtual void Move(Transform m_transform, Rigidbody rigidbody, UnityEngine.Object player)
    {
        // The screen has been touched so store the touch

    }
    public virtual void Move(Transform m_transform)
    {
        // The screen has been touched so store the touch

    } 
}

