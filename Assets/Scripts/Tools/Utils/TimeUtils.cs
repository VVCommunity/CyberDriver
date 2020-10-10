using UnityEngine;

namespace Tools.Utils
{
    public static class TimeUtils
    {
        public static int IntDeltaTime(float time)
        {
            return (int)(1000 * time * Time.deltaTime);
        }
    }
}
