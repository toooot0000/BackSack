using System;
using System.Collections.Generic;
using Components.Damages;
using Components.Effects;
using Components.Stages;
using Components.TileObjects.Effects;
using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects.ForceMovables{
    public abstract class ForceMovable : TileObject, IForceMovable{

        private int GetForceDistance(IForceMovement forceMovement){
            var weight = m_GetModel().Weight;
            var distance =  Math.Clamp(Math.Abs(forceMovement.Force) - weight, 0, 5);
            if (forceMovement.Pulling && forceMovement.Source is ITileObject srcTileObject){
                var srcStagePosition = srcTileObject.GetStagePosition();
                var xDif = Math.Abs(srcStagePosition.x - GetStagePosition().x);
                var yDif = Math.Abs(srcStagePosition.y -GetStagePosition().y);
                distance = Math.Min(distance, Math.Max(xDif, yDif) - 1);
            }
            return distance;
        }
        
        protected virtual IEffect ForcedMoveTo(Vector2Int stagePosition){
            UpdateStagePosition(stagePosition);
            m_GetView().MoveToPosition(stage.StagePositionToWorldPosition(stagePosition));
            
            var ground = stage.GetGround(stagePosition);
            if (ground == null) return null;
            var effect = ground.OnTileObjectEnter(this);
            return Consume(effect);
        }


        protected virtual IEffect FallIntoAna(){
            if (this is IDamageable damageable){
                return damageable.Die();
            }
            return null;
        }

        protected virtual IEffect HitToObstacle(){
            return Consume(new DamageEffect(null, this, new Damage(){
                Element = ElementType.Physic,
                Point = 3
            }));
        }

        public virtual IEffect Consume(IForceMovement effect){
            var distance = GetForceDistance(effect);
            if (distance == 0) return null;
            var direction = effect.Direction;
            for (var i = 1; i <= distance; i++){
                var curPos = m_GetModel().CurrentStagePosition + direction * i;
                switch (stage.GetFloorType(curPos)){
                    case FloorType.Ana:
                        ForcedMoveTo(curPos);
                        return FallIntoAna();
                    case FloorType.Pillar:
                    case FloorType.Stair:
                    case FloorType.Block:
                    case FloorType.Gate:
                        var side = ForcedMoveTo(curPos - direction);
                        if (side == null) return HitToObstacle();
                        var hitSide = HitToObstacle();
                        if (hitSide == null) return side;
                        return new MultiEffect(new[]{ side, hitSide });
                    case FloorType.Empty:
                        // TODO add object collision detection;
                        break;
                }
            }
            ForcedMoveTo(m_GetModel().CurrentStagePosition + direction * distance);
            return null;
        }

        private readonly List<IEffect> _results = new();

        public override IEffect Consume(IEffect effect){
            _results.Clear();
            AddTypedEffectConsumer<IEffect>(_results, effect, base.Consume);
            AddTypedEffectConsumer<IForceMovement>(_results, effect, this);
            return MakeSideEffect(_results);
        }
        private IForceMovableModel m_GetModel() => Model as IForceMovableModel;
        private ForceMovableView m_GetView() => view as ForceMovableView;
    }
}