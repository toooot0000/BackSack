using System;
using System.Collections.Generic;
using Components.Ground;
using Components.Stages;
using Components.TileObjects;
using StageEditor.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;
using EnumUtility = Utility.EnumUtility;

namespace StageEditor{
    public class StageInEditManager: MonoBehaviour{
        public string enemyTileResourcePath = "Tiles/Objects/Enemies/";
        public string groundEffectTileResourcePath = "Tiles/GroundEffects/";
        public string versionNumber = "1.0.0";
        public string stageName = "stage-0";
        public Tilemap floorMap;
        public Tilemap objectMap;
        public Tilemap groundEffectMap;
        public StageTile tileTreasure;

        private Tilemap[] _allMaps = Array.Empty<Tilemap>();

        [HideInInspector] 
        public string loadName = ""; 

        public IEnumerable<Tilemap> AllMaps{
            get{
                if(_allMaps.Length == 0) _allMaps = new[]{ floorMap, objectMap, groundEffectMap };
                return _allMaps;
            }
        }


        private static Dictionary<FloorType, Tile> _typeToFloorTile = null;

        private static Dictionary<FloorType, Tile> TypeToFloorTypeToFloorTile{
            get{
                if (_typeToFloorTile == null){
                    _typeToFloorTile = new();

                    foreach (var o in Resources.LoadAll("Tiles/FloorTiles/")){
                        var tile = (FloorTile)o;
                        _typeToFloorTile[tile.type] = tile;
                    }
                }
                return _typeToFloorTile;
            }
        }


        private Dictionary<int, Tile> _idToEnemyTiles = null;

        private Dictionary<int, Tile> IdToEnemyTile{
            get{
                if (_idToEnemyTiles == null){
                    _idToEnemyTiles = new();
                    foreach (var obj in Resources.LoadAll(enemyTileResourcePath)){
                        var tile = (ObjectTile)obj;
                        _idToEnemyTiles[tile.id] = tile;
                    }
                }
                return _idToEnemyTiles;
            }
        }

        private Dictionary<GroundType, Tile> _typeToGroundEffectTiles = null;

        private Dictionary<GroundType, Tile> TypeToGroundEffectTiles{
            get{
                if (_typeToGroundEffectTiles == null){
                    _typeToGroundEffectTiles = new();
                    foreach (var obj in Resources.LoadAll(groundEffectTileResourcePath)){
                        var stageTile = (GroundTile)obj;
                        _typeToGroundEffectTiles[stageTile.type] = stageTile;
                    }
                }
                return _typeToGroundEffectTiles;
            }
        }


        /// <summary>
        /// Convert current editing result to a Stage model.
        /// </summary>
        /// <returns></returns>
        public Stage ToStage(){
            var size = floorMap.size;
            var ret = new Stage{
                Floors = new Floor[size.x, size.y],
                Meta = new StageMeta{
                    Version = versionNumber,
                    Height = size.y,
                    Width = size.x,
                    Name = stageName
                }
            };
            for (var i = 0; i < size.x; i++){
                for (var j = 0; j < size.y; j++){
                    if (!SetStageFloorType(i, j, ret)) continue;
                    SetStageObject(i, j, ret);
                    SetStageGroundEffect(i, j, ret);
                }
            }
            return ret;
        }
        
        
        /// <summary>
        /// Set up all the maps based on the argument stage. 
        /// </summary>
        /// <param name="source"></param>
        public void FromStage(Stage source){

            foreach (var map in AllMaps){
                map.ClearAllTiles();
            }
            
            versionNumber = source.Meta.Version;
            stageName = source.Meta.Name;
            for (int i = 0; i < source.Width; i++){
                for (int j = 0; j < source.Height; j++){
                    SetMapFloor(i, j, source);
                    SetMapObject(i, j, source);
                    SetMapGroundEffect(i, j, source);
                }
            }
        }

        private void SetMapFloor(int i, int j, Stage src){
            var positionInGrid = src.GetGridPosition(i, j).ToVector3Int();
            floorMap.SetTile(positionInGrid, TypeToFloorTypeToFloorTile[src.Floors[i, j].Type]);
        }

        private void SetMapObject(int i, int j, Stage src){
            var positionInGrid = src.GetGridPosition(i, j).ToVector3Int();
            switch (src.Floors[i, j].TileObjectType){
                case TileObjectType.Enemy:
                    objectMap.SetTile(positionInGrid, IdToEnemyTile[src.Floors[i, j].TileObjectId]);
                    break;
                case TileObjectType.Treasure:
                    objectMap.SetTile(positionInGrid, tileTreasure);
                    break;
            }
        }

        private void SetMapGroundEffect(int i, int j, Stage src){
            var positionInGrid = src.GetGridPosition(i, j).ToVector3Int();
            var type = src.Floors[i, j].GroundType;
            if (type == GroundType.Null) return;
            groundEffectMap.SetTile(positionInGrid, TypeToGroundEffectTiles[type]);
        }

        private bool SetStageFloorType(int i, int j, Stage ret){
            var tile = floorMap.GetTile<FloorTile>(floorMap.GetGridPosition(i, j));
            if(tile == null){
                ret.Floors[i, j] = new Floor{
                    Position = new Vector2Int(i, j),
                    Type = FloorType.Block,
                };
                return false;
            }
            ret.Floors[i, j] = new Floor{
                Position = new Vector2Int(i, j),
                Type = tile.type,
            };
            return true;
        }

        private void SetStageObject(int i, int j, Stage ret){
            // 通过tile 决定类型：tile-player -> Player, tile-enemy-0 -> enemy, tile-chest-x -> chest
            var tile = objectMap.GetTile<ObjectTile>(floorMap.GetGridPosition(i, j));
            if (tile == null){
                ret.Floors[i, j].TileObjectType = TileObjectType.Null;
                return ;
            }

            switch (tile.type){
                case TileObjectType.Enemy:
                    ret.Floors[i, j].TileObjectType = TileObjectType.Enemy;
                    ret.Floors[i, j].TileObjectId = tile.id;
                    break;
                case TileObjectType.Treasure:
                    ret.Floors[i, j].TileObjectType = TileObjectType.Treasure;
                    break;
                case TileObjectType.Null:
                default:
                    break;
            }
        }

        private void SetStageGroundEffect(int i, int j, Stage ret){
            var tile = groundEffectMap.GetTile<GroundTile>(floorMap.GetGridPosition(i, j));
            if (tile == null) return ;
            ret.Floors[i, j].GroundType = tile.type;
        }
    }

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

    internal static class StageExtension{
        public static Vector3Int GetGridPosition(this Stage stage, int row, int col){
            return new Vector3Int(row - stage.Width/2, col - stage.Height/2);
        }
    }

    internal static class Vector2IntExtension{
        public static Vector3Int ToVector3Int(this Vector2Int vec) => new Vector3Int(vec.x, vec.y, 0);
    }
}