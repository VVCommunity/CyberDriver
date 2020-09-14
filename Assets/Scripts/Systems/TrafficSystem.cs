using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Profiling;

public class TrafficSystem : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_prefabs;
    [SerializeField]
    private Transform m_parrentTraffic;
    [SerializeField]
    private int m_countTraafic;
    [SerializeField]
    private Vector3 m_spawnOffSet;
    [SerializeField]
    private Quaternion m_rotationSpawn; 
    [SerializeField]
    private Vector3 m_grideSpawns;
    [SerializeField]
    private float m_speedTrafic;
    private TransformAccessArray m_transformCars;
    private List<Transform> cars = new List<Transform>();

    private void OnEnable()
    {
        for (int i = 0; i < m_countTraafic; i++)
        {
            cars.Add(Instantiate(m_prefabs[Random.Range(0,m_prefabs.Length)], GetPosition(), m_rotationSpawn, m_parrentTraffic));
        }
        m_transformCars = new TransformAccessArray(cars.ToArray());
        Debug.Log($"Load cars: {m_transformCars.length }");
    }
    private void FixedUpdate()
    {
        Profiler.BeginSample("Traffic");
        MoveCars job = new MoveCars(Time.fixedDeltaTime, m_speedTrafic);
        job.Schedule(m_transformCars).Complete();
        Profiler.EndSample();
    }
    private void OnDisable()
    {
        for (int i = 0; i < m_transformCars.length; i++)
        {
            Destroy(m_transformCars[i]);
        }
        m_transformCars.Dispose();
    }
    private Vector3 GetPosition()
    {
        return new Vector3(m_spawnOffSet.x += Random.Range(-m_grideSpawns.x,m_grideSpawns.x), m_spawnOffSet.y += Random.Range(-m_grideSpawns.y, m_grideSpawns.y), m_spawnOffSet.z += Random.Range(-m_grideSpawns.z, m_grideSpawns.z));
    }
}

[BurstCompile]
public struct MoveCars : IJobParallelForTransform
{
    private readonly float m_time;
    private readonly float m_speed;
    public MoveCars(float timeFixedDelta,float speedCars)
    {
        m_time = timeFixedDelta;
        m_speed = speedCars;
    }
    public void Execute(int index, TransformAccess transform)
    {
        transform.position += Vector3.forward * m_speed * m_time;
  
 

        //if(transform.localPosition.y != target[index].x)
        //    transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x, target[index].x), speed);
        //else
        //    transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x, target[index].y), speed);
    }
}
