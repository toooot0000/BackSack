using System;
using System.Collections.Generic;
using Components.Damages;
using Components.Effects;
using Components.Stages;
using Components.TileObjects.Effects;
using Components.TileObjects.StepOverAbles;
using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects.Movables{
    public abstract class Movable: TileObject, IMovable{
        
        /// <summary>
        /// Try move in the given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public virtual IEffect TryMove(Direction direction){
            var dest = CurrentStagePosition + direction.ToVector2Int();
            if (CanMoveToPosition(dest)) return MoveTo(dest);
            m_GetView().BumpToUnsteppable(direction);
            return null;
        }

        private readonly List<IEffect> _sides = new();
        
        
        /// <summary>
        /// Move to position. Do not test if the place is valid or not. 
        /// </summary>
        /// <param name="stagePosition"></param>
        /// <returns>Side effect</returns>
        public IEffect MoveTo(Vector2Int stagePosition){
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
            var ground = stage.GetGround(CurrentStagePosition);
            if (ground == null) return null;
            var effect = ground.OnTileObjectEnter(this);
            return Consume(effect);
        }

        public virtual bool CanMoveToPosition(Vector2Int stagePosition){
            var tileObj = stage.GetTileObject(stagePosition);
            return CanSetPositionForGivenFloorType(stage.GetFloorType(stagePosition))
                   && tileObj is null or IStepOverAble;
        }
        private IMovableView m_GetView() => View as IMovableView;
        
        private int GetForceDistance(IForceMovement forceMovement){
            var weight = Weight;
            var distance =  Math.Clamp(Math.Abs(forceMovement.Force) - weight, 0, 5);
            if (forceMovement.Pulling && forceMovement.Source is ITileObject srcTileObject){
                var srcStagePosition = srcTileObject.CurrentStagePosition;
                var xDif = Math.Abs(srcStagePosition.x - GetStagePosition().x);
                var yDif = Math.Abs(srcStagePosition.y -GetStagePosition().y);
                distance = Math.Min(distance, Math.Max(xDif, yDif) - 1);
            }
            return distance;
        }

        private int GetRemainForce(int movedDistance, IForceMovement forceMovement) =>
            forceMovement.Pulling ? 0 : forceMovement.Force - Weight - movedDistance;
        
        protected virtual IEffect FallIntoAna(){
            if (this is IDamageable damageable){
                return damageable.Die();
            }
            return null;
        }

        protected virtual IEffect HitToObstacle(){
            return Consume(new Damage(null, this, 3, ElementType.Physic));
        }

        public virtual IEffect Consume(IForceMovement effect){
            var distance = GetForceDistance(effect);
            if (distance == 0) return null;
            var direction = effect.Direction.ToVector2Int();
            for (var i = 1; i <= distance; i++){
                var curPos = CurrentStagePosition + direction * i;
                IEffect side;
                switch (stage.GetFloorType(curPos)){
                    case FloorType.Ana:
                        MoveTo(curPos);
                        return FallIntoAna();
                    case FloorType.Pillar:
                    case FloorType.Stair:
                    case FloorType.Block:
                    case FloorType.Gate:
                        side = MoveTo(curPos - direction);
                        if (side == null) return HitToObstacle();
                        var hitSide = HitToObstacle();
                        if (hitSide == null) return side;
                        return new MultiEffect(new[]{ side, hitSide });
                    case FloorType.Empty:
                        var tileObject = stage.GetTileObject(curPos);
                        if (tileObject == null || tileObject == effect.Source) continue;
                        side = MoveTo(curPos - direction);
                        var remainForce = GetRemainForce(i - 1, effect);
                        if (remainForce == 0) return side;
                        if (side == null) return null;
                        return new MultiEffect(new[]{
                            side, new ForceMovement(remainForce, effect.Pulling){
                                Source = effect.Source,
                                Target = tileObject
                            }
                        });
                }
            }
            MoveTo(CurrentStagePosition + direction * distance);
            return null;
        }

        private readonly List<IEffect> _results = new();

        public override IEffect Consume(IEffect effect){
            _results.Clear();
            CallConsumer<IEffect>(_results, effect, base.Consume);
            CallConsumer<IForceMovement>(_results, effect, this);
            return MakeSideEffect(_results);
        }
        
        public abstract int Weight{ get; set; }
    }
}