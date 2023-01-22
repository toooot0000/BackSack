using UnityEngine;

namespace Utility.Extensions{
    public static class ColorExtension{
        public static Color Transparent(this Color c){
            return c.WithAlpha(0);
        }

        public static Color WithAlpha(this Color c, float alpha){
            return new Color(c.r, c.g, c.b, alpha);
        }
    }
}