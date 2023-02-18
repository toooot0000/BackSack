using System;
using Components.Stages.Floors;
using Components.Stages.Templates;
using Components.TileObjects;
using MVC;
using UnityEngine;

namespace Components.Stages{
    
    [Serializable]
    public class StageMeta : Model{
        public string version = "1.0.0";
        public DateTime Time = DateTime.Now;
        public new string name = "stage-0";
        public int width = 3;
        public int height = 3;
        public StageHardness hardness = StageHardness.Normal;
    }
    
    public class StageModel: Model{
        public Floor[,] Floors;
        public StageMeta Meta = new StageMeta();
        public int Width => Meta.width;
        public int Height => Meta.height;

        /**
         * Some more meta data.
         */
        public StageModel(){
            Floors = new Floor[Meta.width, Meta.height];
            for (var i = 0; i < Meta.width; i++){
                for (var j = 0; j < Meta.height; j++){
                    Floors[i, j] = new(new(i, j)){
                        Ground = null,
                        TileObject = null,
                        Type = FloorType.Block
                    };
                }
            }
        }

        public StageModel(StageMeta meta){
            Meta = meta;
            Floors = new Floor[Meta.width, Meta.height];
            for (var i = 0; i < Meta.width; i++){
                for (var j = 0; j < Meta.height; j++){
                    Floors[i, j] = new(new(i, j)){
                        Ground = null,
                        TileObject = null,
                        Type = FloorType.Block
                    };
                }
            }
        }

        public Vector2Int GetGridPosition(int x, int y) => new Vector2Int(x - Meta.width / 2, y - Meta.height / 2);
        public Vector2Int GetGridPosition(Vector2Int stagePosition) => GetGridPosition(stagePosition.x, stagePosition.y);

        public Vector2Int GetStagePosition(Vector2Int gridPosition) =>
            new Vector2Int(gridPosition.x + Meta.width / 2, gridPosition.y + Meta.height / 2);

        public Floor GetFloor(Vector2Int stagePosition){
            return Floors[stagePosition.x, stagePosition.y];
        }
    }
}