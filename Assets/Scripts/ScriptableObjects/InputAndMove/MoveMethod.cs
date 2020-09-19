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

    public virtual void Move(Transform m_transform, Rigidbody rigidbody, UnityEngine.Object player)
    {
    }
}