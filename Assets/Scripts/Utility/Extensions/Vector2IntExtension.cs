using UnityEngine;

namespace Utility.Extensions{
    public static class Vector2IntExtension{
        public static Vector2Int Aligned(this Vector2Int vec){
            return new Vector2(vec.x, vec.y).Aligned();
        }

        public static Vector2Int ToDirection(this Vector2Int vec){
            var aligned = vec.Aligned();
            return aligned.x switch{
                < 0 => Vector2Int.left,
                > 0 => Vector2Int.right,
                _ => aligned.y switch{
                    < 0 => Vector2Int.down,
                    > 0 => Vector2Int.up,
                    _ => Vector2Int.zero
                }
            };
        }

        public static Vector2Int Rotate90DegClockwise(this Vector2Int vec){
            return new(vec.y, -vec.x);
        }
        
        public static Vector2Int Rotate90DegAntiClockwise(this Vector2Int vec){
            return new(-vec.y, vec.x);
        }

        public static Vector2Int Rotate180Deg(this Vector2Int vec){
            return vec * (-1);
        }

        /// <summary>
        /// if on a clock, this vec pointing to a earlier time than the other one
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool IsClockwiseLess(this Vector2Int vec, Vector2Int other){
            if (vec.magnitude == 0 || other.magnitude == 0) return false;
            var z = vec.x * other.y - other.x * vec.y;
            return z < 0;
        }

        public static Vector3Int ToVector3Int(this Vector2Int vec){
            return new Vector3Int(vec.x, vec.y, 0);
        }
    }
}