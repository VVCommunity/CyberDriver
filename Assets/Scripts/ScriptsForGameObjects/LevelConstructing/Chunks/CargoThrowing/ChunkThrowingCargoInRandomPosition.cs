using System;
using System.Collections;
using System.Threading.Tasks;
using Tools.Utils;
using UnityEngine;

namespace ScriptsForGameObjects.LevelConstructing.Chunks.CargoThrowing
{
    public class ChunkThrowingCargoInRandomPosition : SimpleCargoThrowingChunk
    {
        [SerializeField]
        Vector2 targetPositionXConstraints;
        [SerializeField]
        Vector2 targetPositionYConstraints;
        [SerializeField]
        float distanceToPlayerToThrow;

        protected override Vector2 GetTargetThrowingPoint()
        {
            return new Vector2
            {
                x = UnityEngine.Random.Range(targetPositionXConstraints.x, targetPositionXConstraints.y),
                y = UnityEngine.Random.Range(targetPositionYConstraints.x, targetPositionYConstraints.y),
            };
        }

        protected override void OnThrow(Transform throwPoint, Action throwAction)
        {
            var distanceToPlayer = throwPoint.position.z - playerCarMove.Value.transform.position.z;
            if (distanceToPlayer < 0)
            {
                return;
            }
            var distanceToWait = distanceToPlayer - distanceToPlayerToThrow;
            if (distanceToWait > 0)
            {
                var delay = Time.deltaTime *
                    PhysicsUtils.CountDistanceTime(distanceToWait, playerCarMove.Value.ForwardSpeed, playerCarMove.Value.acceleration);
                StartCoroutine(DoAction(delay, throwAction));
            }
            throwAction();
        }

        private IEnumerator DoAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action();
        }
    }
}
