using System;
using System.Collections.Generic;
using Components.Effects;
using Components.Grounds.Effects;
using Components.Stages;
using MVC;
using UnityEngine;

namespace Components.TileObjects{
    public abstract class TileObject: Controller, ITileObject, ICanConsume<MultiEffect>{
        public Stage stage;

        protected override void AfterSetModel(){
            SetStagePosition(m_GetModel().CurrentStagePosition);
        }

        public void SetStagePosition(Vector2Int stagePosition){
            m_GetModel().CurrentStagePosition = stagePosition;
            m_GetView().SetPosition(stage.StagePositionToWorldPosition(stagePosition));
        }

        public Vector2Int GetStagePosition() => m_GetModel().CurrentStagePosition;
        
        public IEffect Move(Vector2Int direction){
            var dest = m_GetModel().CurrentStagePosition + direction;
            if (!IsPositionSteppable(stage.GetFloorType(dest))){
                m_GetView().BumpToUnsteppable(direction);
                return null;
            }
            m_GetModel().CurrentStagePosition = dest;
            m_GetView().MoveToPosition(stage.StagePositionToWorldPosition(dest));
            
            var ground = stage.GetGround(dest);
            if (ground == null) return null;
            var effect = ground.OnTileObjectEnter(this);
            return Consume(effect);
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
        
        public IEffect Consume(MultiEffect effect){
            var ret = new List<IEffect>();
            foreach (var subEff in effect.Effects){
                var subRet = Consume(subEff);
                if(subRet != null) ret.Add(Consume(subEff));
            }
            effect.Effects = ret.ToArray();
            return effect;
        }
        
        
        public virtual IEffect Consume(IEffect effect){
            if (effect is MultiEffect multi) return Consume(multi);
            if (effect is not IGroundEffect groundEffect) return null;
            var ground = stage.GetGround(m_GetModel().CurrentStagePosition);
            if (ground == null) return null;
            return ground.TakeElement(groundEffect.Element, groundEffect.LastTurnNum);
        }
    }
}