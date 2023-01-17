using Components.Effects;
using Components.Items.Effects;
using Components.TileObjects;
using Components.TileObjects.StepOverAbles;
using MVC;
using UnityEngine;

namespace Components.Items{
    public class PickableItem: TileObject, IStepOverAble, IView{

        public SpriteRenderer sprRenderer;

        protected override void Awake(){
            base.Awake();
            view = this;
        }

        public new ItemModel Model{
            set => SetModel(value);
            get => base.Model as ItemModel;
        }

        protected override void AfterSetModel(){
            base.AfterSetModel();
            sprRenderer.sprite = Resources.Load<Sprite>(Model.IconPath);
        }

        [HideInInspector]
        public int Number = 1;
        
        public IEffectTemplate OnSteppedOver(){
            return new ItemChange(Model, Number){
                Source = this
            };
        }
    }
}