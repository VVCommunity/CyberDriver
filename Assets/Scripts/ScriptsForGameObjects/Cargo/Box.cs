using Core.Abstractions;
using Core.Entities;
using System;
using UnityEngine;

namespace ScriptsForGameObjects.Cargo
{
    public class Box : MonoBehaviour, ICargo
    {
        [SerializeField]
        private int worth;
        public int Worth { get => worth; }
        public CargoState State { get; set; }

        public void OnCollisionEnter(Collision collision)
        {
            var tag = collision.gameObject.tag;
            if (tag.Equals("land", StringComparison.InvariantCultureIgnoreCase))
            {
                State = CargoState.ReadyToDrop;
                gameObject.SetActive(false);
            }
        }
    }
}