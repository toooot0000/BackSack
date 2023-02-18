using System.Collections.Generic;
using Components.Grounds;
using Components.Stages.Templates;
using StageEditor.Extensions;
using StageEditor.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StageEditor.MapConverters{
    public class GroundMapConverter: MapConverter{
        public string groundEffectTileResourcePath = "Tiles/GroundEffects/";
        
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
        
        public override void ReadFromTemplate(StageTemplate template){
            foreach (var pair in template.grounds){
                map.SetTile(pair.position.ToVector3Int(), TypeToGroundEffectTiles[pair.value]);
            }
        }

        public override void WriteToTemplate(StageTemplate template){
            template.grounds = map.ProcessTile<GroundTile, PositionValuePair<GroundType>>((i, j, tile, ret) => {
                    ret.Add(new(){
                        position = new(i, j),
                        value = tile.type
                    });
                }).ToArray();
        }

        public override void Clear(){
            map.ClearAllTiles();
            Debug.Log("Ground map cleared!");
        }

        public override void Reload(){
            _typeToGroundEffectTiles = null;
            Debug.Log("Ground map reloaded!");
        }
    }
}