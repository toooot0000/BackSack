using UnityEngine;

namespace Utility.Extensions{
    public static class RectExtension{
        public static void Expand(this Rect rect, Rect other){
            rect.min = Vector2.Min(rect.min, other.min);
            rect.max = Vector2.Max(rect.max, other.max);
        }
    }
}