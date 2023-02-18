using System;
using System.Collections.Generic;
using StageEditor.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility.Extensions;

namespace StageEditor.Extensions{
    internal static class TilemapExtension{
        /// <summary>
        /// Get the grid position based on offsets
        /// </summary>
        public static Vector3Int GetGridPosition(this Tilemap tilemap, int xOffset, int yOffset){
            return tilemap.cellBounds.min + new Vector3Int(xOffset, yOffset);
        }

        public static StageTile GetStageTile(this Tilemap tilemap, int xOffset, int yOffset){
            return tilemap.GetTile<StageTile>(tilemap.GetGridPosition(xOffset, yOffset));
        }

        public static void ProcessTile<T>(this Tilemap tilemap, Action<int, int, T> action) where T: StageTile{
            var bounds = tilemap.cellBounds;
            for (var x = bounds.xMin; x <= bounds.xMax; x++){
                for (var y = bounds.yMin; y <= bounds.yMax; y++){
                    var tile = tilemap.GetTile<T>(new(x, y));
                    if (tile == null) continue;
                    action(x, y, tile);
                }
            }
        }
        
        public static List<TRet> ProcessTile<T, TRet>(this Tilemap tilemap, Action<int, int, T, List<TRet>> action) where T: StageTile{
            var ret = new List<TRet>();
            var bounds = tilemap.cellBounds;
            for (var x = bounds.xMin; x <= bounds.xMax; x++){
                for (var y = bounds.yMin; y <= bounds.yMax; y++){
                    var tile = tilemap.GetTile<T>(new(x, y));
                    if (tile == null) continue;
                    action(x, y, tile, ret);
                }
            }
            return ret;
        }
    }
}