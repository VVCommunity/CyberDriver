using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VV.Core
    {

    public class VVBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected Transform m_transform;
        public Transform GetTransform { get { return m_transform; } }

        protected virtual void Start()
        {
            m_transform = transform;
        }
    }

}