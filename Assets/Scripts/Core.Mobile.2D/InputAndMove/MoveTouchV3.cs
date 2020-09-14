using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Characters", menuName = "Characters/MoveV3")]
public class MoveTouchV3 : MoveMethod
{
    protected bool isEndMove = false;
    public float torque;  
    [SerializeField]

    protected Vector3 newForge;
    public override void Move(Transform m_transform, Rigidbody eventdata,Object player)
    {
         
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isEndMove = false;
                    break;

                case TouchPhase.Moved:
                    Vector3 dir = touch.deltaPosition;
                    // eventdata.position = Vector3.Lerp(eventdata.position, pos, Time.fixedDeltaTime * speed);
                    dir *= speed;
                    //   dir.z = forgeSpeedForward;
                    newForge = dir;
                    eventdata.AddTorque(new Vector3(0,0, -dir.x) * torque); 
                    break;

                case TouchPhase.Ended:
                    isEndMove = true;
                    break;
            }
        }
        else
        {

            newForge = Vector3.zero;
            eventdata.velocity = Vector3.Lerp(eventdata.velocity, new Vector3(0, 0, eventdata.velocity.z), speedEnd);
            eventdata.angularVelocity = Vector3.Lerp(eventdata.angularVelocity, new Vector3(0, 0, 0), speedEnd); 
        }
        if (isEndMove)
        {
            newForge = Vector3.zero; 
            eventdata.velocity = Vector3.Lerp(eventdata.velocity, new Vector3(0, 0, eventdata.velocity.z), speedEnd);
        }
        eventdata.AddForce(newForge);
    }
}
