using System.Collections.Generic;
using Entities.Enemies;
using Models;
using Models.TileObjects;
using MVC;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace Entities.Stages{
    public class StageController: Controller<Stage, IViewWithType<Stage>>, IViewWithType<Stage>, IAfterSetMode{
        public Grid grid;
        public Tilemap floorMap;

        public EnemyManager enemyManager;

        private readonly Dictionary<FloorType, Tile> _tiles = new();

        protected override void Awake(){
            base.Awake();
            view = this;
            foreach (var floor in EnumUtility.GetValues<FloorType>()){
                _tiles[floor] = Resources.Load<Tile>(floor.GetFloorTileResourcePath());
            }
        }

        public void AfterSetModel(){
            floorMap.ClearAllTiles();
            foreach(var floor in Model.Floors){
                SetFloor(floor.Position, floor.Type);
            }
        }

        private void SetFloor(Vector2Int stagePosition, FloorType type){
            var gridPosition = StagePositionToGridPosition(stagePosition);
            floorMap.SetTile(new Vector3Int(gridPosition.x, gridPosition.y, 0), _tiles[type]);
        }

        private void SetTileObject(Floor floor){
            switch (floor.TileObjectType){
                case TileObjectType.Enemy:
                    enemyManager.AddEnemy(Enemy.MakeEnemy(floor.TileObjectId));
                    break;
                case TileObjectType.Treasure:
                    break;
                case TileObjectType.Null:
                default:
                    break;
            }
        }

        public Vector3 GridPositionToWorldPosition(Vector2Int gridPosition){
            return grid.GetCellCenterWorld(new Vector3Int(gridPosition.x, gridPosition.y, 0));
        }

        public Vector2Int StagePositionToGridPosition(Vector2Int stagePosition){
            return new Vector2Int(x: stagePosition.x - Model.Width / 2, y: stagePosition.y + Model.Height / 2);
        }

        public Vector3 StagePositionToWorldPosition(Vector2Int stagePosition){
            return GridPositionToWorldPosition(StagePositionToGridPosition(stagePosition));
        }
        
        public bool IsInStage(Vector2Int position){
            return false; // TODO
        }

        public Vector2Int ClampToStageRange(Vector2Int position){
            return position; // TODO
        }
    }
}