using System;
using UnityEngine;

namespace Tools.Utils
{
    public static class PhysicsUtils
    {
        /// <summary>
        /// Count time needed to cover the distance.
        /// </summary>
        public static float CountDistanceTime(float distance, float baseSpeed, float acceleration = 0)
        {
            var d = Mathf.Pow(baseSpeed, 2) + 2 * acceleration * distance;
            if (d < 0)
            {
                throw new ArgumentException("Invalid arguments.");
            }
            var t1 = (baseSpeed + Mathf.Sqrt(d)) / acceleration;
            var t2 = (baseSpeed - Mathf.Sqrt(d)) / acceleration;
            if (t1 > 0 && t2 < 0)
            {
                return t1;
            }
            if (t2 > 0 && t1 < 0)
            {
                return t2;
            }
            throw new ArgumentException("Invalid arguments.");
        }

        /// <summary>
        /// Count base speed that needed to object flight from start to target point
        /// in vertical plane by desired flight time.
        /// </summary>
        public static Vector2 CountBaseSpeedToTargetFlying(Vector2 startPoint, Vector2 targetPoint, float flightTime)
        {
            float vx = (targetPoint.x - targetPoint.y) / flightTime;
            float vy = (startPoint.y - targetPoint.y + Physics.gravity.y * Mathf.Pow(flightTime, 2) / 2) / flightTime;
            return new Vector2(vx, vy);
        }
    }
}