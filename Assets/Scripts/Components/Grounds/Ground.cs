using System.Collections.Generic;
using Components.Effects;
using Components.Gizmos.ObjectToasts;
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
    public class Ground: Controller, IView{
        public Stage stage;
        public SpriteRenderer sprRenderer;
        public ObjectToaster toaster;

        private GroundModel _model;
        
        public GroundModel Model{
            set{
                if(_model != null) _model.AfterTypeChanged -= UpdateSprite;
                _model = value;
                _model.AfterTypeChanged += UpdateSprite;
                UpdateSprite();
                UpdatePosition();
                _reducer = Reducers[Model.Type];
            }
            get => _model;
        }

        private IReducer _reducer;

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
                } else{
                    toaster.AddToast($"{Model.LastTurnNum.ToString()}", transform, new ToastOptions(){
                        Start = Color.red,
                        End = Color.red
                    });
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