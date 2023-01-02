using System;
using System.Collections.Generic;
using Models.TileObjects;
using MVC;
using UnityEngine;

namespace Models{

    public class StageMeta : Model{
        public string Version = "1.0.0";
        public DateTime Time = DateTime.Now;
        public string Name = "stage-0";
        public int Width = 3;
        public int Height = 3;
    }
    
    public class Stage: Model{
        public Floor[,] Floors;
        public StageMeta Meta = new StageMeta();
        public int Width => Meta.Width;
        public int Height => Meta.Height;

        /**
         * Some more meta data.
         */
        public Stage(){
            Floors = new Floor[Meta.Width, Meta.Height];
            for (var i = 0; i < Meta.Width; i++){
                for (var j = 0; j < Meta.Height; j++){
                    Floors[i, j] = new(){
                        Position = new(i, j),
                        BaseGroundEffect = null,
                        TileObject = null,
                        Type = FloorType.Block
                    };
                }
            }
        }

        public Vector3Int GetGridPosition(int x, int y) => new Vector3Int(x - Meta.Width / 2, y - Meta.Height / 2);

        public ITileObject GetTileObject(Vector2Int position){
            return Floors[position.x, position.y].TileObject;
        }
    }
}