using System;
using Utility.Extensions;

namespace Models.Stages{
    public static partial class FloorTypeExtension{
        public const string ResourcePath = "Tiles/FloorTiles/";
        public const string ResourcePrefix = "tile-";
        public static string GetFloorTileResourcePath(this FloorType floor){
            return $"{ResourcePath}{ResourcePrefix}{floor.GetDescription().ToLower()}";
        }

        public static string GetFloorTileFileName(this FloorType floorType){
            return $"{ResourcePrefix}{floorType.GetDescription().ToLower()}";
        }
        
        public static bool IsSteppable(this FloorType floorType){
            return floorType switch{
                FloorType.Empty => true,
                FloorType.Ana => false,
                FloorType.Block => false,
                FloorType.Gate => true,
                FloorType.Pillar => false,
                FloorType.Stair => true,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}