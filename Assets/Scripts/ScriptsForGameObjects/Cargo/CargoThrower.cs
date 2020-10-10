using Core.Abstractions;
using ScriptsForGameObjects.Car;
using Tools.Utils;
using UnityEngine;

namespace ScriptsForGameObjects.Cargo
{
    public static class CargoThrower
    {
        public static void ThrowCargo(ICargo cargo, Vector3 spawnPoint, Vector2 targetPoint, PlayerCarMove playerCarMove)
        {
            var distanceToPlayer = spawnPoint.z - playerCarMove.transform.position.z;
            var flightTime = PhysicsUtils.CountDistanceTime(distanceToPlayer, playerCarMove.ForwardSpeed, playerCarMove.acceleration);
            var speed = PhysicsUtils.CountBaseSpeedToTargetFlying(spawnPoint, targetPoint, flightTime);

            cargo.rigidbody.position = spawnPoint;
            cargo.rigidbody.AddRelativeForce(speed.x, speed.y, spawnPoint.z, ForceMode.VelocityChange);
        }
    }
}
