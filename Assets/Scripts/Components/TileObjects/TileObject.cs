using System;
using System.Collections.Generic;
using Components.Attacks;
using Components.Effects;
using Components.Grounds;
using Components.Grounds.Effects;
using Components.Stages;
using Components.Stages.Floors;
using MVC;
using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects{
    public abstract class TileObject : Controller, ITileObject{
        
        public abstract Vector2Int CurrentStagePosition{ set; get; }

        public Vector3 Position{
            get => transform.position;
            set => transform.position = value;
        }

        public virtual Vector2Int Size{ get; set; }
        
        public abstract ITileObjectView View{ get; }
        
        /// <summary>
        /// Sent whenever the stage position is changed
        /// </summary>
        public event Action StagePositionUpdated;
        public Stage stage;
        
        
        /// <summary>
        /// Update the stage position of current TileObject. Also inform Stage the change. Will send StagePositionUpdated event.
        /// </summary>
        /// <param name="newStagePosition"></param>
        protected void UpdateStagePosition(Vector2Int newStagePosition){
            if (CurrentStagePosition == newStagePosition) return;
            stage.GetFloor(CurrentStagePosition).TileObject = null;
            stage.GetFloor(newStagePosition).TileObject = this;
            CurrentStagePosition = newStagePosition;
            StagePositionUpdated?.Invoke();
        }
        
        
        /// <summary>
        /// Set the position of the TileObject, both model-wise and view-wise
        /// </summary>
        /// <param name="stagePosition"></param>
        public virtual void SetStagePosition(Vector2Int stagePosition){
            if (!CanSetPositionForGivenFloorType(stage.GetFloorType(stagePosition))) return;
            UpdateStagePosition(stagePosition);
            View.SetPosition(stage.StageToWorldPosition(stagePosition));
        }

        public Vector2Int GetStagePosition() => CurrentStagePosition;
        public Vector3 GetWorldPosition() => stage.StageToWorldPosition(GetStagePosition());
        public Stage GetStage() => stage;

        public virtual bool CanSetPositionForGivenFloorType(FloorType floor){
            return floor switch{
                FloorType.Empty => true,
                FloorType.Ana => false,
                FloorType.Block => false,
                // FloorType.Gate => true,
                // FloorType.Pillar => false,
                // FloorType.Stair => true,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public bool IsDestroyed{ set; get; } = false;

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

        public virtual void Destroy(){
            IsDestroyed = true;
            stage.GetFloor(GetStagePosition()).TileObject = null;
            View.Destroy();
        }
    }
}