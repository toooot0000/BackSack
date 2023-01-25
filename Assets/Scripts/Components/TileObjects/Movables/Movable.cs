using System.Collections.Generic;
using Components.Effects;
using Components.TileObjects.StepOverAbles;
using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects.Movables{
    public class Movable: TileObject, IMovable{
        
        public IEffect Move(Direction direction){
            var dest = m_GetModel().CurrentStagePosition + direction.ToVector2Int();
            if (CanMoveToPosition(dest)) return MoveTo(dest);
            m_GetView().BumpToUnsteppable(direction);
            return null;
        }

        private readonly List<IEffect> _sides = new();
        
        protected IEffect MoveTo(Vector2Int stagePosition){
            UpdateStagePosition(stagePosition);
            m_GetView().MoveToPosition(stage.StageToWorldPosition(stagePosition));
            if (stage.GetTileObject(stagePosition) is IStepOverAble stepOverAble){
                var template = stepOverAble.OnSteppedOver();
                template.Target = this;
                _sides.AddIfNotNull(Consume(template.ToEffect()));
            }
            _sides.AddIfNotNull(EnterGround());
            return MakeSideEffect(_sides);
        }

        private IEffect EnterGround(){
            var ground = stage.GetGround(m_GetModel().CurrentStagePosition);
            if (ground == null) return null;
            var effect = ground.OnTileObjectEnter(this);
            return Consume(effect);
        }

        public virtual bool CanMoveToPosition(Vector2Int stagePosition){
            var tileObj = stage.GetTileObject(stagePosition);
            return IsPositionSteppable(stage.GetFloorType(stagePosition))
                   && tileObj is null or IStepOverAble;
        }
        
        private ITileObjectModel m_GetModel() => Model as ITileObjectModel;
        private IMovableView m_GetView() => view as IMovableView;
    }
}