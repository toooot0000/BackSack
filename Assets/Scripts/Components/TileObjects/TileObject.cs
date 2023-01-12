using System;
using System.Collections.Generic;
using Components.Effects;
using Components.Grounds.Effects;
using Components.Stages;
using MVC;
using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects{
    public abstract class TileObject: Controller, ITileObject, ICanConsume<MultiEffect>{
        public Stage stage;

        protected override void AfterSetModel(){
            SetStagePosition(m_GetModel().CurrentStagePosition);
        }

        protected void UpdateStagePosition(Vector2Int newStagePosition){
            stage.GetFloor(m_GetModel().CurrentStagePosition).TileObject = null;
            stage.GetFloor(newStagePosition).TileObject = this;
            m_GetModel().CurrentStagePosition = newStagePosition;
        }

        public void SetStagePosition(Vector2Int stagePosition){
            UpdateStagePosition(stagePosition);
            m_GetView().SetPosition(stage.StagePositionToWorldPosition(stagePosition));
        }

        public Vector2Int GetStagePosition() => m_GetModel().CurrentStagePosition;
        
        public IEffect Move(Vector2Int stagePosition){
            var dest = m_GetModel().CurrentStagePosition + stagePosition;
            if (!IsPositionSteppable(stage.GetFloorType(dest))){
                m_GetView().BumpToUnsteppable(stagePosition.ToDirection());
                return null;
            }
            UpdateStagePosition(dest);
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
            return null;
        }

        protected static void AddTypedEffectConsumer<TEff>(List<IEffect> ret, IEffect effect,
            Func<TEff, IEffect> consumer) where TEff: IEffect{
            if (effect is not TEff typed) return;
            var side = consumer(typed);
            if (side == null) return;
            ret.Add(side);
        }
        
        protected static void AddTypedEffectConsumer<TEff>(List<IEffect> ret, IEffect effect,
            ICanConsume<TEff> consumer) where TEff: IEffect{
            if (effect is not TEff typed) return;
            var side = consumer.Consume(typed);
            if (side == null) return;
            ret.Add(side);
        }

        protected static IEffect MakeSideEffect(List<IEffect> result){
            return result.Count switch{
                0 => null,
                1 => result[0],
                _ => new MultiEffect(result.ToArray())
            };
        }
    }
}