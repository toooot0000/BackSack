using System.Collections.Generic;
using Components.Damages;
using Components.Effects;
using Components.Grounds.Effects;
using Components.Grounds.Instances;
using Components.Grounds.Reducer;
using Components.Grounds.Triggers;
using Components.Stages;
using Components.TileObjects;
using MVC;
using UnityEngine;
using Utility;
using Utility.Extensions;

namespace Components.Grounds{

    public class Explosion : IDamage, IEffectTemplate{
        public Explosion(){
            Point = 5;
            Element = ElementType.Fire;
        }
        public IEffectConsumer Target{ get; set; }
        public IController Source{ get; set; }
        public IEffect ToEffect() => this;

        public int Point{ set; get; }
        public ElementType Element{ get; }
    }
    
    public class Ground: Controller, IView{
        public Stage stage;
        public SpriteRenderer sprRenderer;
        
        public new GroundModel Model{
            set => SetModel(value);
            get => base.Model as GroundModel;
        }

        private IReducer _reducer;

        protected override void BeforeSetModel(){
            if(base.Model is GroundModel old) old.AfterTypeChanged -= UpdateSprite;
        }

        protected override void AfterSetModel(){
            Model.AfterTypeChanged += UpdateSprite;
            UpdateSprite();
            UpdatePosition();
            _reducer = Reducers[Model.Type];
        }
        
        private static Dictionary<GroundType, IReducer> _reducers = null;
        private static Dictionary<GroundType, IReducer> Reducers{
            get{
                _reducers ??= new Dictionary<GroundType, IReducer>(){
                    { GroundType.Null , null},
                    { GroundType.Fire, new Fire() },
                    { GroundType.Grass , new Grass()},
                    { GroundType.Ice , new Ice()},
                    { GroundType.Oil , new Oil()},
                    { GroundType.Poison , new Poison()},
                    { GroundType.Steam , new Steam()},
                    { GroundType.Water , new Water()}
                };
                return _reducers;
            }
        }
        

        public void SetType(GroundType newType){
            Model.Type = newType;
            _reducer = _reducers[newType];
            UpdateSprite();
        }

        public void SetPosition(Vector2Int stagePosition){
            Model.Position = stagePosition;
            UpdatePosition();
        }

        public IEffectTemplate TakeElement(ElementType element, int lastTurn = -1){
            if (_reducer == null) return null;
            
            Model.LastTurnNum = lastTurn;
            var ret = _reducer.TakeElement(this, element);
            if(Model.Type == GroundType.Null) gameObject.SetActive(false);
            return ret;
        }

        public IEffect OnTurnEnd(ITileObject tileObject){
            if (Model.LastTurnNum > 0){
                Model.LastTurnNum--;
                if (Model.LastTurnNum == 0){
                    Model.Type = GroundType.Null;
                    _reducer = null;
                }
            }
            if(_reducer is IOnTurnEnd end) return end.OnTurnEnd(this, tileObject);
            return null;
        }

        public IEffect OnTileObjectEnter(ITileObject tileObject){
            if(_reducer is IOnTileObjectEnter enter) return enter.OnTileObjectEnter(this, tileObject);
            return null;
        }

        public IEffect OnTileObjectExit(ITileObject tileObject){
            if (_reducer is IOnTileObjectExit exit) return exit.OnTileObjectExit(this, tileObject);
            return null;
        }

        private static string GroundTypeToImagePath(GroundType type){
            return $"Images/Tiles/GroundEffects/Ground-{type.GetDescription().ToLower()}";
        }


        private static Dictionary<GroundType, Sprite> _sprites;

        private static Dictionary<GroundType, Sprite> Sprites{
            get{
                if (_sprites == null){
                    _sprites = new();
                    foreach (var groundType in EnumUtility.GetValues<GroundType>()){
                        _sprites[groundType] = Resources.Load<Sprite>(GroundTypeToImagePath(groundType));
                    }
                }
                return _sprites;
            }
        }
        
        private void UpdateSprite(){
            sprRenderer.sprite = Sprites[Model.Type];
        }

        private void UpdatePosition(){
            transform.position = stage.StageToWorldPosition(Model.Position);
        }
    }
}