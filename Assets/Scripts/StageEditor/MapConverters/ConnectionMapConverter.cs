using System;
using System.Collections.Generic;
using System.Linq;
using Components.Stages.Templates;
using StageEditor.Extensions;
using StageEditor.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility.Extensions;

namespace StageEditor.MapConverters{
    public class ConnectionMapConverter: MapConverter{
        
        
        
        [Serializable]
        public struct DirectionTilePair{
            public Direction dir;
            public Tile tile;
        }

        public DirectionTilePair[] directionalConnectionPairs;
        private Dictionary<Direction, Tile> _tiles;

        private Dictionary<Direction, Tile> Tiles{
            get{
                _tiles ??= directionalConnectionPairs.Aggregate(new Dictionary<Direction, Tile>(), (tiles, pair) => {
                    tiles[pair.dir] = pair.tile;
                    return tiles;
                });
                return _tiles;
            }
        }

        public override void ReadFromTemplate(StageTemplate template){
            foreach (var pair in template.connectedDirection){
                map.SetTile(pair.position.ToVector3Int(), Tiles[pair.value]);
            }
        }

        public override void WriteToTemplate(StageTemplate template){
            template.connectedDirection = map.ProcessTile<ConnectionTile, PositionValuePair<Direction>>(
                (i, j, tile, ret) => {
                    ret.Add(new(){
                        position = new(i, j),
                        value = tile.direction
                    });
                }).ToArray();
        }

        public override void Clear(){
            map.ProcessTile<ConnectionTile>((i, j, tile) => {
                map.SetTile(new(i, j), null);
            });
        }

        public override void Reload(){
            _tiles = null;
            Debug.Log("Connection map reloaded!");
        }
    }
}