using System;
using Components.Effects;
using Components.Items.Effects;
using Components.TileObjects;
using Components.TileObjects.StepOverAbles;
using MVC;
using UnityEngine;

namespace Components.Items{
    public class PickableItem: TileObject, IStepOverAble, ITileObjectView{

        public SpriteRenderer sprRenderer;

        private ItemModel _model;
        public ItemModel Model{
            set{
                _model = value;
                sprRenderer.sprite = Resources.Load<Sprite>(value.IconPath);
            }
            get => _model;
        }


        [NonSerialized]
        public int Number = 1;
        
        public IEffectTemplate OnSteppedOver(){
            Destroy(this);
            return new ItemChange(Model, Number);
        }

        public override Vector2Int CurrentStagePosition{ get; set; }
        public override ITileObjectView View => this;
        
        public void SetPosition(Vector3 worldPosition){
            throw new NotImplementedException();
        }
    }
}