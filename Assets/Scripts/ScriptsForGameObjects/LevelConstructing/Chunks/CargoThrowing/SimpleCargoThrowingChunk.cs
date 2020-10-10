using Core;
using Core.Entities;
using ScriptsForGameObjects.Car;
using ScriptsForGameObjects.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools.Common;
using UnityEngine;

namespace ScriptsForGameObjects.LevelConstructing.Chunks.CargoThrowing
{
    public abstract class SimpleCargoThrowingChunk : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> pointsFromThrowCargos;
        [SerializeField]
        private int maxCargosToThrowInChunk;
        [SerializeField]
        private GameObject player;

        protected Cached<PlayerCarMove> playerCarMove { get; private set; }
        protected int throwedCargosCount { get; private set; }

        public void Awake()
        {
            playerCarMove = new Cached<PlayerCarMove>(player);
        }

        public void Update()
        {
            if (throwedCargosCount >= maxCargosToThrowInChunk)
            {
                return;
            }
            var cargo = CargoManager.GetCargoReadyForDrop();
            if (cargo != null)
            {
                var point = pointsFromThrowCargos.ElementAt(UnityEngine.Random.Range(0, pointsFromThrowCargos.Count));
                OnThrow(point, () =>
                {
                    CargoThrower.ThrowCargo(cargo, point.position, GetTargetThrowingPoint(), playerCarMove.Value);
                    cargo.State = CargoState.Drop;
                    cargo.gameObject.SetActive(true);
                    ++throwedCargosCount;
                });
            }
        }

        protected abstract Vector2 GetTargetThrowingPoint();

        /// <summary>
        /// Control throwing.
        /// </summary>
        protected virtual void OnThrow(Transform throwPoint, Action throwAction) => throwAction();
    }
}
