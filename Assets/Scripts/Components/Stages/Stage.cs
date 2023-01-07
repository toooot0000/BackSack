using System;
using Components.TileObjects;
using MVC;
using UnityEngine;

namespace Components.Stages{

    public class StageMeta : Model{
        public string Version = "1.0.0";
        public DateTime Time = DateTime.Now;
        public new string Name = "stage-0";
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
                        Ground = null,
                        TileObject = null,
                        Type = FloorType.Block
                    };
                }
            }
        }

        public Vector2Int GetGridPosition(int x, int y) => new Vector2Int(x - Meta.Width / 2, y - Meta.Height / 2);
        public Vector2Int GetGridPosition(Vector2Int stagePosition) => GetGridPosition(stagePosition.x, stagePosition.y);

        public Vector2Int GetStagePosition(Vector2Int gridPosition) =>
            new Vector2Int(gridPosition.x + Meta.Width / 2, gridPosition.y + Meta.Height / 2);

        public Floor GetFloor(Vector2Int stagePosition){
            return Floors[stagePosition.x, stagePosition.y];
        }

        public ITileObjectModel GetTileObject(Vector2Int stagePosition){
            return GetFloor(stagePosition).TileObject;
        }
    }
}