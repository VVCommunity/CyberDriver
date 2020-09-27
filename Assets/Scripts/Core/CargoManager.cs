using Assets.Scripts.Core.Abstractions;
using Assets.Scripts.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CargoManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> prefabsToSpawn;
    private static List<GameObject> prefabs = new List<GameObject>();
    private static List<ICargo> cargoObjects = new List<ICargo>();

    private void Start()
    {
        // Unfortunately, we cannot see static fields in the inspector :c
        prefabs = prefabsToSpawn;
    }

    public static GameObject GetCargoReadyForDrop()
    {
        var cargo = cargoObjects.FirstOrDefault(c => c.Condition == CargoCondition.ReadyToDrop);
        if (cargo == null)
        {
            var index = Random.Range(0, prefabs.Count);
            var obj = Instantiate(prefabs[index]);
            obj.SetActive(false);
            cargoObjects.Add(obj.GetComponent<ICargo>());
            return obj;
        }
        return cargo.gameObject;
    }
}
