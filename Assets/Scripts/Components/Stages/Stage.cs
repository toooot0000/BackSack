using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Components.Effects;
using Components.Enemies;
using Components.Grounds;
using Components.Grounds.Effects;
using Components.TileObjects;
using Components.TreasureBoxes;
using MVC;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Utility;

namespace Components.Stages{
    public class Stage: Controller, IView{
        public Grid grid;
        public Tilemap floorMap;
        public EnemyManager enemyManager;
        public GameObject treasureBoxPrefab;
        public GameObject groundPrefab;
        public ClickHandler clickHandler;

        private readonly Dictionary<FloorType, Tile> _tiles = new();

        private new StageModel Model{
            set => base.SetModel(value);
            get => base.Model as StageModel;
        }

        protected override void Awake(){
            base.Awake();
            clickHandler.Action = OnClickStagePosition;
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
                    var enemy = new EnemyModel(floor.TileObjectId);
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

        public ITileObject GetTileObject(Vector2Int stagePosition) => Model.GetFloor(stagePosition).TileObject;

        public Floor GetFloor(Vector2Int stagePosition) => Model.GetFloor(stagePosition);

        public IEnumerable<Floor> GetFloors(){
            foreach (var modelFloor in Model.Floors){
                yield return modelFloor;
            }
        }

        #region Helper Functions

        public Vector3 GridToWorldPosition(Vector2Int gridPosition){
            return grid.GetCellCenterWorld(new Vector3Int(gridPosition.x, gridPosition.y, 0));
        }

        public Vector2Int StageToGridPosition(Vector2Int stagePosition) => Model.GetGridPosition(stagePosition);

        public Vector2Int GridToStagePosition(Vector2Int gridPosition) => Model.GetStagePosition(gridPosition);

        public Vector3 StageToWorldPosition(Vector2Int stagePosition){
            return GridToWorldPosition(StageToGridPosition(stagePosition));
        }

        public Vector2Int WorldToStagePosition(Vector3 worldPosition){
            var cell = grid.WorldToCell(worldPosition);
            return GridToStagePosition(new Vector2Int(cell.x, cell.y));
        }
        
        public bool IsPositionInStage(Vector2Int stagePosition){
            return stagePosition.x >= 0 && stagePosition.x < Model.Width && stagePosition.y >= 0 && stagePosition.y <Model.Height;
        }

        public Vector2Int ClampToStageRange(Vector2Int stagePosition){
            return new Vector2Int(Math.Clamp(stagePosition.x, 0, Model.Width), Math.Clamp(stagePosition.y, 0, Model.Height));
        }

#endregion


#region Ground

        private List<Ground> _grounds = new();

        /// <summary>
        /// Called during stage setup procedure
        /// </summary>
        /// <param name="floor"></param>
        private void CreateGround(Floor floor){
            if (floor.GroundType == GroundType.Null) return;
            var ground = MakeGround(floor.GroundType);
            SetGround(floor.Position, ground);
        }

        public void SetGround(Vector2Int stagePosition, Ground ground){
            ground.SetPosition(stagePosition);
            GetFloor(stagePosition).Ground = ground;
        }
        
        /// <summary>
        /// Make an empty ground with given type and stagePosition;
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stagePosition"></param>
        /// <returns></returns>
        public Ground MakeGround(GroundType type){
            foreach (var ground in _grounds.Where(ground => !ground.gameObject.activeSelf)){
                ground.gameObject.SetActive(true);
                return ground;
            }
            var ret = Instantiate(groundPrefab, transform).GetComponent<Ground>();
            ret.stage = this;
            ret.Model = new GroundModel(){
                Type = type,
                LastTurnNum = 10
            };
            _grounds.Add(ret);
            return ret;
        }

        public Ground GetGround(Vector2Int stagePosition) => GetFloor(stagePosition).Ground;

        public IEnumerable<Ground> GetGrounds() => _grounds;

#endregion
        
        public IEffect TileObjectEnterPosition(ITileObject tileObject, Vector2Int position){
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
        
        /// <summary>
        /// Moving the target searching procedure here to enable the chance of optimization. 
        /// </summary>
        /// <param name="stagePosition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<ITileObject> GetTileObject(Vector2Int stagePosition, Predicate<ITileObject> predicate){
            throw new NotImplementedException();
        }
        
        
        /// <summary>
        /// Stage consume a ground effect at given stage position;
        /// </summary>
        /// <param name="groundEffect"></param>
        /// <param name="stagePosition"></param>
        /// <returns></returns>
        public IEffect Consume(IGroundEffect groundEffect, Vector2Int stagePosition){
            var ground = GetGround(stagePosition);
            if (ground != null && ground.Model.Type != GroundType.Null){
                var explosion =  ground.TakeElement(groundEffect.Element, groundEffect.LastTurnNum);
                if (explosion == null) return null;
                explosion.Target = GetTileObject(stagePosition);
                return explosion.Target?.Consume(explosion.ToEffect());
            }
            if (groundEffect is not ICreateNewGround createNew) return null;
            if (GetFloorType(stagePosition) != FloorType.Empty) return null;
            if (ground == null){
                ground = MakeGround(createNew.GroundType);
                SetGround(stagePosition, ground);
            } else{
                ground.SetType(createNew.GroundType);
            }
            ground.Model.LastTurnNum = createNew.LastTurnNum;
            return ground.OnTileObjectEnter(GetTileObject(stagePosition));
        }

        private void OnClickStagePosition(Vector2Int stagePosition){
            Debug.Log($"Click Position: {stagePosition.ToString()}");
        }
    }
}