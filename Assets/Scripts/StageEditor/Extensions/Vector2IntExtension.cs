using UnityEngine;

namespace StageEditor{
    internal static class Vector2IntExtension{
        public static Vector3Int ToVector3Int(this Vector2Int vec) => new Vector3Int(vec.x, vec.y, 0);
    }
}