using System;
using Models.Ground;
using Models.TileObjects;
using MVC;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace Models.Stages{
    public class Floor: Model{
        public Vector2Int Position;
        public FloorType Type = FloorType.Block;
        public TileObjectType TileObjectType = TileObjectType.Null;
        public int TileObjectId = 0;
        public string GroundEffectName = "";
        
        [JsonIgnore]
        public Ground.Ground Ground = null;
        [JsonIgnore]
        public ITileObject TileObject = null;
    }
}