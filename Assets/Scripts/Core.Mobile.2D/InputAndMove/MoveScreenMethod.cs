using UnityEngine;

[CreateAssetMenu(fileName = "Characters", menuName = "Characters/MoveTouch")]
public class MoveScreenMethod : MoveMethod
{ 
    [SerializeField]
    private Vector2 startPos;   
    [Range(0, 100)]
    public float sensaX = 1;
    [Range(0, 100)]
    public float sensaY = 1; 
    int finderId = -1;
    bool isFinder = false;

    public override void  Enable(Transform m_transform)
    { 
        startPos = m_transform.position;
    }
    public override void Move(Transform m_transform, Rigidbody2D rigidbody)
    {

        if (Input.touchCount > 0)
        { 
            Touch touch = Input.GetTouch(0);
            if (!isFinder)
            {
                finderId = touch.fingerId;
                Debug.Log("Finder touch id:" + finderId);
                isFinder = true;
            }
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = Input.GetTouch(0).position;
                    break;
                case TouchPhase.Ended:
                    break;
            }
            if (touch.fingerId != finderId)
            {
                Debug.Log("NEW TOUCH!");
                finderId = touch.fingerId;
            }
            else
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            { 
                
                Vector2 dir = Input.GetTouch(0).position - startPos; // переменной - объекту присваиваеться переменная с координатами мыши
              //      rigidbody.position = Vector3.MoveTowards(rigidbody.position, objPosition, speeding); // и собственно объекту записываються координаты
                
                dir.x *= sensaX;
                dir.y *= sensaY; 
                //   dir.x = Mathf.Clamp(dir.x, -5, 5f);
                //   dir.x = Mathf.Clamp(dir.x, -5, 5f); 
                rigidbody.AddForce(new Vector2(dir.x, dir.y));
            }
            else
            {
                // rigidbody.velocity = Vector2.zero;
                //dir = Vector2.zero;//= Vector2.Lerp(rigidbody.velocity,Vector2.zero,0.2f);
                //   rigidbody.velocity = Vector2.Lerp(rigidbody,Vector2.zero,0.2f);
            }
            startPos = Input.GetTouch(0).position;
        }
        else
        {
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, Vector2.zero, 0.15f);
        }

        return;

    }
    public Vector2 GetTouchPos(Transform m_transform)
    {
        Vector3 mousePosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, m_transform.position.z); // переменной записываються координаты мыши по иксу и игрику
        Vector2 dir = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
        return dir;
    } 
}
