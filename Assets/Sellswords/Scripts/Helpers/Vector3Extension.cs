using UnityEngine;

namespace Sellswords
{
    public static class Vector3Extension
    {
        public static float CalcDistance(this Vector3 from, Vector3 to)
        {
            var distanceX = to.x - from.x;
            var distanceY = to.y - from.y;
            var distanceZ = to.z - from.z;

            return distanceX * distanceX + distanceY * distanceY + distanceZ * distanceZ;
        }
    }
}