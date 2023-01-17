using System;
using System.Collections.Generic;
using Components.Attacks;
using Components.Effects;
using Components.Grounds;
using Components.Grounds.Effects;
using Components.Stages;
using MVC;
using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects{
    public abstract class TileObject : Controller, ITileObject{
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
        public Vector3 GetWorldPosition() => stage.StagePositionToWorldPosition(GetStagePosition());
        public Stage GetStage() => stage;

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
                if (subRet != null) ret.Add(Consume(subEff));
            }

            effect.Effects = ret.ToArray();
            return effect;
        }


        public virtual IEffect Consume(IEffect effect){
            if (effect is MultiEffect multi) return Consume(multi);
            return null;
        }

        protected static void CallConsumer<TEff>(List<IEffect> ret, IEffect effect,
            Func<TEff, IEffect> consumer) where TEff : IEffect{
            if (effect is not TEff typed) return;
            var side = consumer(typed);
            if (side == null) return;
            ret.Add(side);
        }

        protected static void CallConsumer<TEff>(List<IEffect> ret, IEffect effect,
            ICanConsume<TEff> consumer) where TEff : IEffect{
            if (effect is not TEff typed) return;
            var side = consumer.Consume(typed);
            if (side == null) return;
            ret.Add(side);
        }

        protected static IEffect MakeSideEffect(List<IEffect> result){
            return IEffect.MakeSideEffect(result);
        }

        public IEnumerable<ITileObject> SearchTargets(IAttack attack){
            var curCount = 0;
            foreach (var stagePos in attack.TargetPositions){
                var target = stage.GetTileObject(stagePos);
                if (target == null || !attack.TargetPredicate(target)) continue;
                if (attack.TargetNum > 0 && curCount >= attack.TargetNum) yield break;
                curCount++;
                yield return target;
            }
        }
    }
}