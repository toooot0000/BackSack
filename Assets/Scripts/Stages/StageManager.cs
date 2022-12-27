using System.Collections.Generic;
using Models;
using MVC;
using Newtonsoft.Json;
using UnityEditor.Android;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace Stages{
    public class StageManager: Manager<Stage, IViewWithType<Stage>>, IViewWithType<Stage>, IAfterSetMode{
        public Grid grid;
        public Tilemap floorMap;
        public Tilemap tileObjectsMap;
        public Tilemap groundEffectMap;
        
        private readonly Dictionary<FloorType, Tile> _tiles = new();

        protected override void Awake(){
            base.Awake();
            view = this;
            foreach (var floor in EnumUtility.GetValues<FloorType>()){
                _tiles[floor] = Resources.Load<Tile>(floor.GetFloorTileResourcePath());
            }
        }

        private void Start(){
            
        }

        public void AfterSetModel(){
            floorMap.ClearAllTiles();
            tileObjectsMap.ClearAllTiles();
            groundEffectMap.ClearAllTiles();
            foreach(var floor in model.Floors){
                SetFloor(floor.Position, floor.Type);
            }
        }

        public void SetFloor(Vector2Int stagePosition, FloorType type){
            var gridPosition = StagePositionToGridPosition(stagePosition);
            floorMap.SetTile(new Vector3Int(gridPosition.x, gridPosition.y, 0), _tiles[type]);
        }

        public Vector3 GridPositionToWorldPosition(Vector2Int gridPosition){
            return grid.GetCellCenterWorld(new Vector3Int(gridPosition.x, gridPosition.y, 0));
        }

        public Vector2Int StagePositionToGridPosition(Vector2Int stagePosition){
            return new Vector2Int(x: stagePosition.x - model.Width / 2, y: stagePosition.y + model.Height / 2);
        }
    }
}