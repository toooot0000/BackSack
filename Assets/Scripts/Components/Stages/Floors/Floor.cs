using Components.Grounds;
using Components.TileObjects;
using MVC;
using Newtonsoft.Json;
using UnityEngine;

namespace Components.Stages.Floors{
    public class Floor: Model{
        public readonly Vector2Int Position;
        public FloorType Type = FloorType.Block;
        public TileObjectType TileObjectType = TileObjectType.Null;
        public int TileObjectId = 0;
        public GroundType GroundType = GroundType.Null;
        
        [JsonIgnore]
        public Ground Ground = null;
        [JsonIgnore]
        public ITileObject TileObject = null;

        public Floor(Vector2Int position){
            Position = position;
        }
    }
}