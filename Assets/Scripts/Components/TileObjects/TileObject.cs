using System;
using Components.Effects;
using Components.Stages;
using MVC;
using UnityEngine;

namespace Components.TileObjects{
    public abstract class TileObject: Controller, ITileObject{
        public Stage stage;
        
        protected override void AfterSetModel(){
            SetPosition(GetModel().CurrentStagePosition);
        }

        public void SetPosition(Vector2Int stagePosition){
            GetModel().CurrentStagePosition = stagePosition;
            GetView().SetPosition(stage.StagePositionToWorldPosition(stagePosition));
        }
        
        public bool Move(Vector2Int direction){
            var dest = GetModel().CurrentStagePosition + direction;
            if (!IsPositionSteppable(stage.GetFloorType(dest))){
                GetView().BumpToUnsteppable(direction);
                return false;
            }
            GetModel().CurrentStagePosition = dest;
            GetView().MoveToPosition(stage.StagePositionToWorldPosition(dest));
            return true;
        }
        
        public bool CanMoveToPosition(Vector2Int stagePosition){
            return IsPositionSteppable(stage.GetFloorType(stagePosition)) 
                   && stage.GetTileObject(stagePosition) == null;
        }

        public virtual bool IsPositionSteppable(FloorType floor){
            return floor switch{
                FloorType.Empty => true,
                FloorType.Ana => false,
                FloorType.Block => false,
                FloorType.Gate => true,
                FloorType.Pillar => false,
                FloorType.Stair => true,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected abstract ITileObjectModel GetModel();
        protected abstract ITileObjectView GetView();
        public abstract IEffectResult[] Consume(IEffect effect);
    }
}