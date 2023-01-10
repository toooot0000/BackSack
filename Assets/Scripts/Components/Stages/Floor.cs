using Components.Grounds;
using Components.TileObjects;
using MVC;
using Newtonsoft.Json;
using UnityEngine;

namespace Components.Stages{
    public class Floor: Model{
        public Vector2Int Position;
        public FloorType Type = FloorType.Block;
        public TileObjectType TileObjectType = TileObjectType.Null;
        public int TileObjectId = 0;
        public GroundType GroundType = GroundType.Null;
        
        [JsonIgnore]
        public Ground Ground = null;
        [JsonIgnore]
        public ITileObject TileObject = null;
    }
}