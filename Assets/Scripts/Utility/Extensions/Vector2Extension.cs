using UnityEngine;

namespace Utility.Extensions{
    public static class Vector2Extension{
        /// <summary>
        /// Rotate the vector in degrees, clockwise; 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static Vector2 Rotated(this Vector2 v, float degrees){
            var sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            var cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            var tx = v.x;
            var ty = v.y;
            v.x = cos * tx - sin * ty;
            v.y = sin * tx + cos * ty;
            return v;
        }
        
        /// <summary>
        /// Align the vector to axis directions
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vector2Int AlignedDirection(this Vector2 vec){
            if (vec.magnitude == 0) return Vector2Int.zero;
            vec = vec.Rotated(-45);
            if (vec.x > 0){
                if (vec.y > 0)
                    return Vector2Int.up;
                return Vector2Int.right;
            }

            if (vec.y > 0)
                return Vector2Int.left;
            return Vector2Int.down;
        }

        public static Vector2 Aligned(this Vector2 vec){
            var dir = AlignedDirection(vec);
            return vec.magnitude * new Vector2(dir.x, dir.y);
        }
        
        public static bool IsClockwiseLess(this Vector2 vec, Vector2 other){
            if (vec.magnitude == 0 || other.magnitude == 0) return false;
            var z = vec.x * other.y - other.x * vec.y;
            return z < 0;
        }
    }
}