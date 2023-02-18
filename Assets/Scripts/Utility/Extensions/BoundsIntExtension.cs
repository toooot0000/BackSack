using System.Collections.Generic;
using UnityEngine;

namespace Utility.Extensions{
    public static class BoundsIntExtension{
        public static IEnumerable<Vector2Int> GetPositions(this BoundsInt bounds){
            for (var x = bounds.xMin; x <= bounds.xMax; x++){
                for (var y = bounds.yMin; y <= bounds.yMax; y++){
                    yield return new Vector2Int(x, y);
                }
            }
        }
    }
}