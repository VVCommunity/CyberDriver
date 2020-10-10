using Core.Abstractions;
using Core.Entities;
using System;
using Tools.Common;
using UnityEngine;

namespace ScriptsForGameObjects.Cargo
{
    public class Box : MonoBehaviour, ICargo
    {
        [SerializeField]
        private int worth;
        public int Worth { get => worth; }
        public CargoState State { get; set; }

        private Cached<Rigidbody> cachedRigidbody;
        public new Rigidbody rigidbody => cachedRigidbody.Value;

        public void Awake()
        {
            cachedRigidbody = new Cached<Rigidbody>(gameObject);
        }

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