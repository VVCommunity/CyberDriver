using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VV.Core;
using VV.Core.Mobile2D;

public delegate void OnRespawn(Car car);

public delegate void OnMoveCar();
public delegate void OnCarMove();
public class Car : Player
{
    public static OnRespawn OnRespawn;
    public static OnMoveCar OnMoveCar;
    [SerializeField]
    private Vector3 m_speedForward;
    [SerializeField]
    private Rigidbody m_rigidbody;
    [SerializeField]
    public Transform m_centerMass;
    private Vector3 m_spawnPosition;
    private Vector3 m_spawnSpeedForward;
    [Header("HitBox")]
    [SerializeField]
    float m_slowCar = 1;
    private void Awake()
    {
        if(m_rigidbody && m_centerMass)
        {
            m_rigidbody.centerOfMass = m_centerMass.localPosition;
        } 
    }
    protected override void Start()
    {
        base.Start();
        m_spawnPosition = m_transform.position;
        m_spawnSpeedForward = m_speedForward;
    }
    
    protected override void FixedUpdate()
    {
        if (isMove)
        {
            OnMoveCar?.Invoke();
            m_transform.Translate(m_speedForward * Time.fixedDeltaTime);
            methodMove.Move(m_transform, m_rigidbody,this);
        }
    }
    
    public virtual void HitBoxSlow()
    {
        m_speedForward *= m_slowCar;
    }
    public virtual void HitBox()
    {
        StartCoroutine(
        Respawn());
    }
    protected virtual IEnumerator Respawn()
    {
        var TimeOff = Time.timeScale;
        Time.timeScale *= 0.5f;
        yield return new WaitForSeconds(2);
        Time.timeScale = TimeOff;
        OnRespawn?.Invoke(this);
        
        isMove = false;
        m_transform.position = m_spawnPosition;
        m_speedForward = m_spawnSpeedForward;
    }
}
