using System;
using UnityEngine;

namespace Utility.Extensions{
    public static class FloatExtension{
        public static bool AlmostEquals(this float a, float other){
            return Mathf.Abs(a - other) <= Mathf.Epsilon;
        }

        public static void MaxOne(this float f, float other){
            f = Mathf.Max(f, other);
        }

        public static void MinOne(this float f, float other){
            f = Mathf.Min(f, other);
        }
    }
}