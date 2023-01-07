using System;
using System.Collections.Generic;
using Components.Enemies;
using Components.TileObjects;
using Components.TreasureBox;
using MVC;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace Components.Stages{
    public class StageController: Controller, IView{
        public Grid grid;
        public Tilemap floorMap;
        public EnemyManager enemyManager;
        public GameObject treasureBoxPrefab;

        private readonly Dictionary<FloorType, Tile> _tiles = new();

        protected override void Awake(){
            base.Awake();
            foreach (var floor in EnumUtility.GetValues<FloorType>()){
                _tiles[floor] = Resources.Load<Tile>(floor.GetFloorTileResourcePath());
            }
        }

        protected override void AfterSetModel(){
            floorMap.ClearAllTiles();
            foreach(var floor in GetModel().Floors){
                SetFloor(floor.Position, floor.Type);
                SetTileObject(floor);
            }
        }

        private void SetFloor(Vector2Int stagePosition, FloorType type){
            var gridPosition = GetModel().GetGridPosition(stagePosition.x, stagePosition.y);
            floorMap.SetTile(new Vector3Int(gridPosition.x, gridPosition.y, 0), _tiles[type]);
        }

        private void SetTileObject(Floor floor){
            switch (floor.TileObjectType){
                case TileObjectType.Enemy:
                    var enemy = Enemy.MakeEnemy(floor.TileObjectId);
                    enemy.CurrentStagePosition = floor.Position;
                    floor.TileObject = enemy;
                    enemyManager.AddEnemy(enemy);
                    Debug.Log($"{floor.Position.ToString()}: set file object to Enemy");
                    break;
                case TileObjectType.Treasure:
                    var box = Instantiate(treasureBoxPrefab, transform).GetComponent<TreasureBoxController>();
                    box.stageController = this;
                    box.SetModel(new TreasureBox.TreasureBox(){
                        CurrentStagePosition = floor.Position
                    });
                    floor.TileObject = box.GetModel();
                    Debug.Log($"{floor.Position.ToString()}: set file object to Treasure");
                    break;
                case TileObjectType.Null:
                default:
                    break;
            }
        }

        #region Helper Functions

        public Vector3 GridPositionToWorldPosition(Vector2Int gridPosition){
            return grid.GetCellCenterWorld(new Vector3Int(gridPosition.x, gridPosition.y, 0));
        }

        public Vector2Int StagePositionToGridPosition(Vector2Int stagePosition) => GetModel().GetGridPosition(stagePosition);

        public Vector2Int GridPositionToStagePosition(Vector2Int gridPosition) => GetModel().GetStagePosition(gridPosition);

        public Vector3 StagePositionToWorldPosition(Vector2Int stagePosition){
            return GridPositionToWorldPosition(StagePositionToGridPosition(stagePosition));
        }
        
        public bool IsPositionInStage(Vector2Int stagePosition){
            return stagePosition.x >= 0 && stagePosition.x < GetModel().Width && stagePosition.y >= 0 && stagePosition.y <GetModel().Height;
        }

        public Vector2Int ClampToStageRange(Vector2Int stagePosition){
            return new Vector2Int(Math.Clamp(stagePosition.x, 0, GetModel().Width), Math.Clamp(stagePosition.y, 0, GetModel().Height));
        }

        #endregion

        public Stage GetModel() => Model as Stage;
    }
}