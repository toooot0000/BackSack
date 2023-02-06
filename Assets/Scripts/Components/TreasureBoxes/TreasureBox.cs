using Components.Effects;
using Components.Stages;
using Components.TileObjects;
using MVC;
using UnityEngine;
using Utility.Extensions;

namespace Components.TreasureBoxes{
    public class TreasureBox: TileObject, ITileObjectView{
        public TreasureBoxModel Model{ set; get; }

        public void SetModel(TreasureBoxModel model){
            Model = model;
            transform.position = stage.StageToWorldPosition(Model.CurrentStagePosition);
        }
        

        public void MoveToPosition(Vector3 worldPosition){
            throw new System.NotImplementedException();
        }

        public void BumpToUnsteppable(Direction direction){
            throw new System.NotImplementedException();
        }

        public void SetPosition(Vector3 worldPosition){
            throw new System.NotImplementedException();
        }

        public override Vector2Int CurrentStagePosition{ get; set; }
        public override ITileObjectView View => this;
    }
}