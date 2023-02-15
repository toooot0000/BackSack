using System;
using System.Collections.Generic;
using Components.Grounds;
using Components.Stages;
using Components.TileObjects;
using StageEditor.Tiles;
using UnityEditor;
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

        public readonly Converter Converter = new Converter();

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
        public StageModel ToStageModel(){
            return Converter.ToStageModel(this);
        }
        
        
        /// <summary>
        /// Set up all the maps based on the argument stage. 
        /// </summary>
        /// <param name="source"></param>
        public void LoadFromStageModel(StageModel source){
            Converter.LoadFromStageModel(this, source);
        }

        public void SetMapFloorFromModel(int i, int j, StageModel src){
            var positionInGrid = src.GetGridPosition(i, j).ToVector3Int();
            floorMap.SetTile(positionInGrid, TypeToFloorTypeToFloorTile[src.Floors[i, j].Type]);
        }

        public void SetMapObjectFromModel(int i, int j, StageModel src){
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

        public void SetMapGroundEffectFromModel(int i, int j, StageModel src){
            var positionInGrid = src.GetGridPosition(i, j).ToVector3Int();
            var type = src.Floors[i, j].GroundType;
            if (type == GroundType.Null) return;
            groundEffectMap.SetTile(positionInGrid, TypeToGroundEffectTiles[type]);
        }

        public bool SetFloorTypeInModel(int i, int j, StageModel ret){
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

        public void SetObjectInModel(int i, int j, StageModel ret){
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

        public void SetGroundEffectInModel(int i, int j, StageModel ret){
            var tile = groundEffectMap.GetTile<GroundTile>(floorMap.GetGridPosition(i, j));
            if (tile == null) return ;
            ret.Floors[i, j].GroundType = tile.type;
        }
    }
}