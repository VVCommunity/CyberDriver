using UnityEngine;

namespace Core.Abstractions
{
    public interface ISpawnable
    {
        /// <summary>
        /// Spawn object.
        /// </summary>
        /// <param name="z">Required minimal z-value of object's location to spawn.</param>
        /// <returns>Spawned object.</returns>
        GameObject Spawn(float? z = null);

        void Unspawn();
    }
}
