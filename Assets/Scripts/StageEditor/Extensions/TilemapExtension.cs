using StageEditor.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StageEditor{
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
    }
}