using System;
using System.Collections.Generic;
using Components.Stages.Templates;
using Components.TileObjects;
using StageEditor.Extensions;
using StageEditor.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StageEditor.MapConverters{
    [RequireComponent(typeof(Tilemap))]
    public class ObjectMapConverter: MapConverter{
        public string enemyTileResourcePath = "Tiles/Objects/Enemies/";
        
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
        
        [Serializable]
        public struct TilePair{
            public TileObjectType type;
            public Tile tile;
        }

        public TilePair[] tiles;


        private readonly Dictionary<TileObjectType, Tile> _tiles = new();
        private Dictionary<TileObjectType, Tile> TypeToTile{
            get{
                if (_tiles.Count > 0) return _tiles;
                foreach (var tilePair in tiles){
                    _tiles[tilePair.type] = tilePair.tile;
                }
                return _tiles;
            }
        }

        public override void ReadFromTemplate(StageTemplate template){
            foreach (var pair in template.tileObjects){
                switch (pair.value.type){
                    case TileObjectType.Null:
                        break;
                    case TileObjectType.Enemy:
                        map.SetTile(pair.position.ToVector3Int(), IdToEnemyTile[pair.value.id]);
                        break;
                    case TileObjectType.Treasure:
                    default:
                        var tile = TypeToTile[pair.value.type];
                        map.SetTile(pair.position.ToVector3Int(), tile);
                        break;
                }
            }
        }
        
        public override void WriteToTemplate(StageTemplate template){
            
            template.tileObjects = map.ProcessTile<ObjectTile, PositionValuePair<TileObjectInfo>>((i, j, tile, ret) => {
                ret.Add(new(){
                    position = new(i, j),
                    value = new(){
                        id = tile.id,
                        type = tile.type
                    }
                });
            }).ToArray();
            
        }

        public override void Reload(){
            _idToEnemyTiles = null;
            Debug.Log("Object map reloaded!");
        }

        public override void Clear(){
            map.ClearAllTiles();
            Debug.Log("Object map cleared!");
        }
    }
}