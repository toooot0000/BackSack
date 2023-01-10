using Components.Effects;
using Components.Stages;
using Components.TileObjects;
using MVC;
using UnityEngine;

namespace Components.TreasureBoxes{
    public class TreasureBox: TileObject, ITileObjectView{

        protected override void AfterSetModel(){
            transform.position = stage.StagePositionToWorldPosition(GetModel().CurrentStagePosition);
        }

        protected override ITileObjectModel GetModel() => Model as TreasureBoxModel;

        protected override ITileObjectView GetView() => this;
        public override IEffectResult[] Consume(IEffect effect){
            throw new System.NotImplementedException();
        }

        public void MoveToPosition(Vector3 worldPosition){
            throw new System.NotImplementedException();
        }

        public void BumpToUnsteppable(Vector2Int direction){
            throw new System.NotImplementedException();
        }

        public void SetPosition(Vector3 worldPosition){
            throw new System.NotImplementedException();
        }
    }
}