using System;
using System.Collections.Generic;
using Components.Effects;
using Components.Enemies;
using Components.Grounds;
using Components.TileObjects;
using Components.TreasureBoxes;
using MVC;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace Components.Stages{
    public class Stage: Controller, IView{
        public Grid grid;
        public Tilemap floorMap;
        public EnemyManager enemyManager;
        public GameObject treasureBoxPrefab;
        public GameObject groundPrefab;

        private readonly Dictionary<FloorType, Tile> _tiles = new();

        public new StageModel Model{
            set => base.SetModel(value);
            get => base.Model as StageModel;
        }

        protected override void Awake(){
            base.Awake();
            foreach (var floor in EnumUtility.GetValues<FloorType>()){
                _tiles[floor] = Resources.Load<Tile>(floor.GetFloorTileResourcePath());
            }
        }

        protected override void AfterSetModel(){
            floorMap.ClearAllTiles();
            foreach(var floor in Model.Floors){
                CreateFloor(floor.Position, floor.Type);
                CreateTileObject(floor);
                CreateGround(floor);
            }
        }


        private void CreateFloor(Vector2Int stagePosition, FloorType type){
            var gridPosition = Model.GetGridPosition(stagePosition.x, stagePosition.y);
            floorMap.SetTile(new Vector3Int(gridPosition.x, gridPosition.y, 0), _tiles[type]);
        }

        public FloorType GetFloorType(Vector2Int stagePosition) => Model.GetFloor(stagePosition).Type;

        private void CreateTileObject(Floor floor){
            switch (floor.TileObjectType){
                case TileObjectType.Enemy:
                    var enemy = EnemyModel.MakeEnemy(floor.TileObjectId);
                    enemy.CurrentStagePosition = floor.Position;
                    floor.TileObject = enemyManager.AddEnemy(enemy);
                    Debug.Log($"{floor.Position.ToString()}: set file object to Enemy");
                    break;
                case TileObjectType.Treasure:
                    var box = Instantiate(treasureBoxPrefab, transform).GetComponent<TreasureBox>();
                    box.stage = this;
                    box.SetModel(new TreasureBoxModel(){
                        CurrentStagePosition = floor.Position
                    });
                    floor.TileObject = box;
                    Debug.Log($"{floor.Position.ToString()}: set file object to Treasure");
                    break;
                case TileObjectType.Null:
                default:
                    break;
            }
        }

        public ITileObject GetTileObject(Vector2Int stagePosition){
            return Model.GetFloor(stagePosition).TileObject;
        }

#region Helper Functions

        public Vector3 GridPositionToWorldPosition(Vector2Int gridPosition){
            return grid.GetCellCenterWorld(new Vector3Int(gridPosition.x, gridPosition.y, 0));
        }

        public Vector2Int StagePositionToGridPosition(Vector2Int stagePosition) => Model.GetGridPosition(stagePosition);

        public Vector2Int GridPositionToStagePosition(Vector2Int gridPosition) => Model.GetStagePosition(gridPosition);

        public Vector3 StagePositionToWorldPosition(Vector2Int stagePosition){
            return GridPositionToWorldPosition(StagePositionToGridPosition(stagePosition));
        }
        
        public bool IsPositionInStage(Vector2Int stagePosition){
            return stagePosition.x >= 0 && stagePosition.x < Model.Width && stagePosition.y >= 0 && stagePosition.y <Model.Height;
        }

        public Vector2Int ClampToStageRange(Vector2Int stagePosition){
            return new Vector2Int(Math.Clamp(stagePosition.x, 0, Model.Width), Math.Clamp(stagePosition.y, 0, Model.Height));
        }

#endregion


#region Ground

        private void CreateGround(Floor floor){
            if (floor.GroundType == GroundType.Null) return;
            var ground = MakeGround(floor.GroundType, floor.Position);
            SetGround(floor.Position, ground);
        }

        public void SetGround(Vector2Int stagePosition, Ground ground){
            var floor = Model.GetFloor(stagePosition);
            floor.Ground = ground;
            ground.Floor = floor;
        }
        
        /// <summary>
        /// Make an empty ground with given type and stagePosition;
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stagePosition"></param>
        /// <returns></returns>
        public Ground MakeGround(GroundType type, Vector2Int stagePosition){
            var ret = Instantiate(groundPrefab, transform).GetComponent<Ground>();
            ret.stage = this;
            ret.Model = new GroundModel(){
                Type = type,
                LastTurnNum = 10,
                Position = stagePosition
            };
            return ret;
        }

        public Ground GetGround(Vector2Int stagePosition) => Model.GetFloor(stagePosition).Ground;
        
#endregion
        
        public IEffect OnTileObjectEnterPosition(ITileObject tileObject, Vector2Int position){
            var ground = GetGround(position);
            if (ground == null) return null;
            return ground.OnTileObjectEnter(tileObject);
        }
        
        /// <summary>
        /// range.min.x <= ret.x <= range.max.x;
        /// range.min.y <= ret.y <= range.max.y;
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public IEnumerable<ITileObject> GetTileObjectInRange(RectInt range){
            var start = range.min;
            var end = range.max;
            for (var i = start.x; i <= end.x; i++){
                for (var j = start.y; j <= end.y; j++){
                    if (Model.Floors[i, j].TileObject == null) continue;
                    yield return Model.Floors[i, j].TileObject;
                }
            }
        }
    }
}