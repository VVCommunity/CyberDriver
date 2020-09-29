using Assets.Scripts.Core.Abstractions;
using Assets.Scripts.Core.Entities;
using EasyButtons;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CargoManager : MonoBehaviour
{
    [SerializeField]
    private float timeGapForCargoSpawn = 10f;
    [SerializeField]
    private List<GameObject> prefabsToSpawn;
    private static List<GameObject> prefabs = new List<GameObject>();
    private static List<ICargo> cargoObjects = new List<ICargo>();

    private void Start()
    {
        prefabs = prefabsToSpawn;
        StartAddCargoTimer();
    }

    public void StartAddCargoTimer()
    {
        StartCoroutine(PreparingCargoForDrop());
    }

    public void StopAddCargoTimer()
    {
        StopCoroutine(PreparingCargoForDrop());
    }

    private IEnumerator PreparingCargoForDrop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeGapForCargoSpawn);
            AddNewCargo();
        }
    }

    private void AddNewCargo()
    {
        var index = Random.Range(0, prefabs.Count);
        var obj = Instantiate(prefabs[index]);
        obj.SetActive(false);
        cargoObjects.Add(obj.GetComponent<ICargo>());
    }

    public GameObject GetCargoReadyForDrop()
    {
        var cargo = cargoObjects.FirstOrDefault(c => c.Condition == CargoState.ReadyToDrop);
        return cargo?.gameObject;
    }

    [Button]
    public void GetCargoExample()
    {
        var box = GetCargoReadyForDrop();
        if (box == null)
        {
            return;
        }
        box.GetComponent<ICargo>().Condition = CargoState.Drop;
        var v = new Vector3(0, 10, 10);
        box.transform.position = v;
        box.SetActive(true);
    }
}
