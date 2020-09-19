
#if UNITY_EDITOR
using EasyButtons;
#endif
using System.Collections.Generic; 
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Profiling;
public class LevelConstructor : MonoBehaviour
{
    public Transform car;
    [SerializeField]
    private Transform[] prefabs;

    [SerializeField]
    private int step;
    public Quaternion rotationLeft;
    public Quaternion rotationRight;

    [SerializeField]
    private Vector3 offSet;

    [SerializeField]
    protected Transform m_transform;

    [SerializeField]
    private Vector3 gridSpawnLevel;

    [Header("EditorGizmos")]
    [SerializeField]
    private Vector3 size;
    [SerializeField]
    private Vector3 offSetDraw;

    [Header("Generator level")]
    [SerializeField]
    private List<Transform> m_objectsLeft = new List<Transform>();
    [SerializeField]
    private List<Transform> m_objectsRight = new List<Transform>();
    [SerializeField]
    private int offSetCarPosition;
    [SerializeField]
    private Vector3 pointLeft;
    [SerializeField]
    private Vector3 pointRight;
    [SerializeField]
    private bool IsGenerator = false; 
 
    JobHandle handle;
    private TransformAccessArray objsLeft;
    private TransformAccessArray objsRight;
    private void OnEnable()
    { 
        objsLeft = new TransformAccessArray(m_objectsLeft.ToArray());
        objsRight = new TransformAccessArray(m_objectsRight.ToArray());
    }
#if UNITY_EDITOR
    [Button]
#endif
    public void LoadObjects()
    {
        for (int i = 0; i < step / 2; i++)
        {
            m_objectsLeft.Add(
            Instantiate(prefabs[Random.Range(0, prefabs.Length - 1)], pointLeft, rotationLeft, m_transform));
            pointLeft += gridSpawnLevel;
        }
        for (int i = 0; i < step / 2; i++)
        {
            m_objectsRight.Add(
            Instantiate(prefabs[Random.Range(0, prefabs.Length - 1)], pointRight, rotationRight, m_transform));
            pointRight += gridSpawnLevel;
        }
    }
#if UNITY_EDITOR
    [Button]
#endif
    public void Rest()
    {
        m_objectsLeft.Clear();
        m_objectsRight.Clear();
        pointLeft = new Vector3(0, 0, 0);
        pointRight = new Vector3(12.5f, 0, 0);
    }

   
    private void Update()
    {
        if (!IsGenerator) return;
        Profiler.BeginSample("LevelsMove");

        for (int i = 0; i < m_objectsLeft.Count; i++)
        {
            if ((car.position.z + offSetCarPosition) > m_objectsLeft[i].position.z)
            {
                ObjToPointRight(m_objectsLeft[i]);
            }
        }

        for (int i = 0; i < m_objectsRight.Count; i++)
        {
            if ((car.position.z + offSetCarPosition) > m_objectsRight[i].position.z)
            {
                ObjToPointLeft(m_objectsRight[i]);
            }
        }
        Profiler.EndSample();
    }
    private void ObjToPointRight(Transform objWorld)
    {
        objWorld.position = pointRight;
        pointRight += gridSpawnLevel;

    }
    private void ObjToPointLeft(Transform objWorld)
    {
        objWorld.position = pointLeft;
        pointLeft += gridSpawnLevel;
    }
#if UNITY_EDITOR
    [Button]
#endif
    public void Redo()
    {
        offSet += gridSpawnLevel;
    }
#if UNITY_EDITOR
    [Button]
#endif
    public void Undo()
    {
        offSet -= gridSpawnLevel;
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pointRight, 1);
        Gizmos.DrawWireCube(pointRight + offSetDraw, size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointRight + gridSpawnLevel, 1);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pointLeft, 1);
        Gizmos.DrawWireCube(pointLeft + offSetDraw, size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointLeft + gridSpawnLevel, 1); 
    }
    private void OnDisable()
    {
        objsLeft.Dispose();
        objsRight.Dispose();
    }
 
 
    private bool RandomBool()
    {
        return Random.Range(0, 2) == 0;
    } 

} 
public struct Generator : IJobParallelForTransform
{ 
    public readonly float carZ;
    public readonly int offSetCarPosition;
    public Vector3 gridSpawnLevel; 
    public NativeArray<Vector3> point; 

    public Generator(float _carZ, int _offSetCar, Vector3 _grid, NativeArray<Vector3> _point)
    { 
        carZ = _carZ;
        offSetCarPosition = _offSetCar;
        gridSpawnLevel = _grid;
        point = _point;

    }

    public void Execute(int index, TransformAccess transform)
    {
        Debug.Log(index); 
        if ((carZ + offSetCarPosition) > transform.position.z)
        { 
            transform.position = point[index];
            point[index] += gridSpawnLevel;
        }

        //if(transform.localPosition.y != target[index].x)
        //    transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x, target[index].x), speed);
        //else
        //    transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x, target[index].y), speed);
    }
}