using System.Collections.Generic;
using Components.Effects;
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
    public class Ground: Controller, IView{
        public Stage stage;
        public SpriteRenderer sprRenderer;
        
        public new GroundModel Model{
            set{
                if(base.Model is GroundModel old) old.AfterTypeChanged -= UpdateSprite;
                SetModel(value);
                value.AfterTypeChanged += UpdateSprite;
                UpdateSprite();
                UpdatePosition();
            }
            get => base.Model as GroundModel;
        }

        public Floor Floor;
        private IReducer _reducer;
        public ITileObject TileObject => Floor.TileObject;

        protected override void AfterSetModel(){
            SetReducer();
        }
        
        private static Dictionary<GroundType, IReducer> _reducers = null;
        private static Dictionary<GroundType, IReducer> Reducers{
            get{
                _reducers ??= new Dictionary<GroundType, IReducer>(){
                    { GroundType.Null , null},
                    { GroundType.Fire, new Fire() },
                    { GroundType.Explosion , new Explosion()},
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
        private void SetReducer(){
            _reducer = Reducers[Model.Type];
        } 
        
        
        public void TakeElement(ElementType element, int lastTurn){
            if (_reducer == null) return;
            Model.Type = _reducer.TakeElement(element);
            _reducer = Reducers[Model.Type];
            Model.LastTurnNum = Model.Type == GroundType.Explosion ? 1 : lastTurn; // Explosion only last one turn
        }

        public IEffect OnTurnEnd(){
            Model.LastTurnNum--;
            if (Model.LastTurnNum == 0){
                Model.Type = GroundType.Null;
            }
            if(_reducer is IOnTurnEnd end) return end.OnTurnEnd(this, TileObject);
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
            transform.position = stage.StagePositionToWorldPosition(Model.Position);
        }
    }
}