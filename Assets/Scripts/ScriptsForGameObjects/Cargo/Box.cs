using Assets.Scripts.Core.Abstractions;
using Assets.Scripts.Core.Entities;
using UnityEngine;

public class Box : MonoBehaviour, ICargo
{
    [SerializeField]
    private int worth;
    public int Worth { get => worth; }
    public CargoState Condition { get; set; }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Land"))
        {
            Condition = CargoState.ReadyToDrop;
            gameObject.SetActive(false);
        }
    }
}
