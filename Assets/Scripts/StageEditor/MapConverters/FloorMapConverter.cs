using System.Collections.Generic;
using Components.Stages.Floors;
using Components.Stages.Templates;
using StageEditor.Extensions;
using StageEditor.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StageEditor.MapConverters{
    public class FloorMapConverter: MapConverter{

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
        
        public override void ReadFromTemplate(StageTemplate template){
            foreach (var positionValuePair in template.floors){
                map.SetTile(positionValuePair.position.ToVector3Int(), TypeToFloorTypeToFloorTile[positionValuePair.value]);
            }
        }

        public override void WriteToTemplate(StageTemplate template){
            var ret = map.ProcessTile<FloorTile, PositionValuePair<FloorType>>(((i, j, tile, res) => {
                res.Add(new(){
                    position = new(i, j),
                    value = tile.type
                });
            }));

            template.floors = ret.ToArray();
        }

        public override void Clear(){
            map.ClearAllTiles();
            Debug.Log("Floor map cleared!");
        }

        public override void Reload(){
            _typeToFloorTile = null;
            Debug.Log("Floor map reloaded!");
        }
    }
}