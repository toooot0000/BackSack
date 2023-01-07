using System;
using Components.Effects;
using Components.Stages;
using MVC;
using UnityEngine;

namespace Components.TileObjects{
    public interface ITileObjectView : IView{
        void MoveToPosition(Vector3 worldPosition);
        void BumpToUnsteppable(Vector2Int direction);
    }

    public abstract class TileObjectController: Controller, IEffectConsumer{
        public StageController stageController;
        public bool Move(Vector2Int direction){
            var dest = GetModel().CurrentStagePosition + direction;
            if (!IsPositionSteppable(stageController.GetModel().GetFloor(dest))){
                GetView().BumpToUnsteppable(direction);
                return false;
            }
            GetModel().CurrentStagePosition = dest;
            GetView().MoveToPosition(stageController.StagePositionToWorldPosition(dest));
            return true;
        }
        
        public bool CanMoveToPosition(Vector2Int stagePosition){
            var stage = stageController.GetModel();
            return IsPositionSteppable(stage.GetFloor(stagePosition)) 
                   && stage.GetTileObject(stagePosition) == null;
        }

        public virtual IEffectResult[] Consume(IEffect effect){
            return null;
        }
        
        public virtual bool IsPositionSteppable(Floor floor){
            return floor.Type switch{
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
        
        
    }
}