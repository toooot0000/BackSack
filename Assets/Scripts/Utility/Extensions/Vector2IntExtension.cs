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
    }
}