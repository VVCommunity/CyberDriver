using System;
using UnityEngine;

namespace Tools.Common
{
    public class Cached<T> where T : class
    {
        private readonly GameObject _target;
        private bool _isCached;
        private T _component;

        public Cached(GameObject target)
        {
            _target = target != null ? target : throw new ArgumentNullException(nameof(target));
        }

        public T Value
        {
            get
            {
                if (!_isCached)
                {
                    _component = _target.GetComponent<T>();
                    _isCached = true;
                }
                return _component;
            }
        }

        /// <summary>
        /// Additionally, check that game object exists.
        /// </summary>
        public T SafeValue
        {
            get
            {
                if (_target)
                {
                    if (!_isCached)
                    {
                        _component = _target.GetComponent<T>();
                    }
                    return _component;
                }
                return null;
            }
        }

        public static explicit operator T(Cached<T> cached) => cached.Value;
    }
}
