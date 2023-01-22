using Components.Effects;
using Components.Stages;
using Components.TileObjects;
using MVC;
using UnityEngine;

namespace Components.TreasureBoxes{
    public class TreasureBox: TileObject, ITileObjectView{
        public new TreasureBoxModel Model{
            set => base.SetModel(value);
            get => base.Model as TreasureBoxModel;
        }
        
        protected override void Awake(){
            base.Awake();
            base.view = this;
        }

        protected override void AfterSetModel(){
            transform.position = stage.StageToWorldPosition(Model.CurrentStagePosition);
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