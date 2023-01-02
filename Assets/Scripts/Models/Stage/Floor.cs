using System;
using System.ComponentModel;
using Models;
using Models.GroundEffects;
using Models.TileObjects;
using MVC;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;
using Utility.Extensions;

namespace Models{
    public enum FloorType{
        /// <summary>
        /// 空地
        /// </summary>
        [Description("empty")]
        Empty,
        [Description("ana")]
        Ana,
        /// <summary>
        /// 墙壁
        /// </summary>
        [Description("block")]
        Block,
        [Description("pillar")]
        Pillar,
        [Description("gate")]
        Gate,
        [Description("stair")]
        Stair,
    }

    public enum TileObjectType{
        [Description("null")]
        Null,
        [Description("enemy")]
        Enemy,
        [Description("treasureBox")]
        Treasure,
    }

    public class Floor: Model{
        public Vector2Int Position;
        public FloorType Type = FloorType.Block;
        public TileObjectType TileObjectType = TileObjectType.Null;
        public int TileObjectId = 0;
        public string GroundEffectName = "";
        
        [JsonIgnore]
        public BaseGroundEffect BaseGroundEffect = null;
        [JsonIgnore]
        public ITileObject TileObject = null;
    } 
}

public static class FloorExtension{
    [Obsolete]
    public static string ToResourcePath(this Floor floor) => floor.Type.GetFloorTileResourcePath();
    
}

public static class FloorTypeExtension{
    public const string ResourcePath = "Tiles/FloorTiles/";
    public const string ResourcePrefix = "tile-";
    public static string GetFloorTileResourcePath(this FloorType floor){
        return $"{ResourcePath}{ResourcePrefix}{floor.GetDescription().ToLower()}";
    }

    public static string GetFloorTileFileName(this FloorType floorType){
        return $"{ResourcePrefix}{floorType.GetDescription().ToLower()}";
    }
}