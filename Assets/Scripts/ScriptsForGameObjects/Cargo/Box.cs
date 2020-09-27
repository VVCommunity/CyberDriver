using Assets.Scripts.Core.Abstractions;
using Assets.Scripts.Core.Entities;
using UnityEngine;

public class Box : MonoBehaviour, ICargo
{
    [SerializeField]
    private int worth;
    public int Worth { get => worth; }
    [SerializeField]
    private CargoCondition condition;
    public CargoCondition Condition 
    { 
        get => condition; 
        set 
        {
            condition = value;
        } 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Land"))
        {
            Condition = CargoCondition.ReadyToDrop;
            gameObject.SetActive(false);
        }
    }
}
