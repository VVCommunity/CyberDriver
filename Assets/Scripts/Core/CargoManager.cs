using Core.Abstractions;
using Core.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class CargoManager : MonoBehaviour
    {
        [SerializeField]
        private float timeGapForCargoSpawn = 10f;
        [SerializeField]
        private List<GameObject> prefabsToSpawn;
        private static List<GameObject> prefabs = new List<GameObject>();
        private static readonly List<ICargo> cargos = new List<ICargo>();

        private void Start()
        {
            prefabs = prefabsToSpawn;
            StartAddCargoTimer();
            GameManager.OnGamePaused += StopAddCargoTimer;
            GameManager.OnGameResumed += StartAddCargoTimer;
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
            cargos.Add(obj.GetComponent<ICargo>());
        }

        public GameObject GetCargoReadyForDrop()
        {
            return cargos.FirstOrDefault(c => c.State == CargoState.ReadyToDrop)?.gameObject;
        }
    }
}