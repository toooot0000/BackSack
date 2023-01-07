using Utility.Extensions;

namespace Components.Stages{
    public static partial class FloorTypeExtension{
        public const string ResourcePath = "Tiles/FloorTiles/";
        public const string ResourcePrefix = "tile-";
        public static string GetFloorTileResourcePath(this FloorType floor){
            return $"{ResourcePath}{ResourcePrefix}{floor.GetDescription().ToLower()}";
        }

        public static string GetFloorTileFileName(this FloorType floorType){
            return $"{ResourcePrefix}{floorType.GetDescription().ToLower()}";
        }
    }
}