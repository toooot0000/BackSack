using Utility.Extensions;

namespace Components.Stages.Floors{
    public static partial class FloorTypeExtension{
        public const string ResourcePath = "Tiles/FloorTiles/";
        public const string ResourcePrefix = "";
        public static string GetFloorTileResourcePath(this FloorType floor){
            return $"{ResourcePath}{ResourcePrefix}{floor.GetDescription().ToLower()}";
        }

        public static string GetFloorTileFileName(this FloorType floorType){
            return $"{ResourcePrefix}{floorType.GetDescription().ToLower()}";
        }
    }
}