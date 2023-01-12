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

        public bool CanForcedIntoPosition(Vector2Int stagePosition){
            return false; // TODO
        }

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


        public virtual IEffect FallIntoAna(){
            if (this is IDamageable damageable){
                return damageable.Die();
            }
            return null;
        }
        public virtual IEffect HitToObstacle(){
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
                }
            }
            ForcedMoveTo(m_GetModel().CurrentStagePosition + direction * distance);
            return null;
        }

        private readonly List<IEffect> _results = new();

        public override IEffect Consume(IEffect effect){
            _results.Clear();
            var baseRet = base.Consume(effect);
            if(baseRet != null) _results.Add(base.Consume(effect));
            if (effect is IForceMovement forceMovement) _results.Add(Consume(forceMovement));
            if (_results.Count == 0) return null;
            if (_results.Count == 1) return _results[0];
            return new MultiEffect(_results.ToArray());
        }
        private IForceMovableModel m_GetModel() => Model as IForceMovableModel;
        private ForceMovableView m_GetView() => view as ForceMovableView;
    }
}