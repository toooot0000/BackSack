using System;
using System.Collections.Generic;
using Components.Effects;
using Components.Stages;
using MVC;
using UnityEngine;

namespace Components.TileObjects{
    public abstract class TileObject: Controller, ITileObject, ICanConsume<IMultiEffect>{
        public Stage stage;

        protected override void AfterSetModel(){
            SetPosition(m_GetModel().CurrentStagePosition);
        }

        public void SetPosition(Vector2Int stagePosition){
            m_GetModel().CurrentStagePosition = stagePosition;
            m_GetView().SetPosition(stage.StagePositionToWorldPosition(stagePosition));
        }
        
        public bool Move(Vector2Int direction){
            var dest = m_GetModel().CurrentStagePosition + direction;
            if (!IsPositionSteppable(stage.GetFloorType(dest))){
                m_GetView().BumpToUnsteppable(direction);
                return false;
            }
            m_GetModel().CurrentStagePosition = dest;
            m_GetView().MoveToPosition(stage.StagePositionToWorldPosition(dest));
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

        private ITileObjectModel m_GetModel() => Model as ITileObjectModel;
        private ITileObjectView m_GetView() => view as ITileObjectView;
        public IEffectResult Consume(IMultiEffect effect){
            var ret = new List<IEffectResult>();
            foreach (var subEff in effect.Effects){
                ret.AddRange(Consume(subEff));
            }
            return new MultiEffectResult(effect, this, ret.ToArray());
        }

        public virtual IEffectResult[] Consume(IEffect effect){
            if (effect is IMultiEffect multi) return new[] { Consume(multi) };
            return Array.Empty<IEffectResult>();
        }
    }
}