using UnityEngine;

namespace Utility.Extensions{
    public static class Vector2IntExtension{
        public static Vector2Int Aligned(this Vector2Int vec){
            return new Vector2(vec.x, vec.y).Aligned();
        }
    }
}