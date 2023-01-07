using UnityEngine;

namespace Utility.Extensions{
    public static class ColorExtension{
        public static Color ToTransparent(this Color c){
            return new Color(c.r, c.g, c.b, 0);
        }
    }
}