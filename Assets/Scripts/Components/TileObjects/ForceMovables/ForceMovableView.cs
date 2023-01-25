using Components.TileObjects.Movables;
using Components.TileObjects.Tweens;
using UnityEngine;
using Utility.Animation;
using Utility.Extensions;

namespace Components.TileObjects.ForceMovables{
    public interface IForceMovableView : IMovableView{
        
    }
    
    public class ForceMovableView: AnimationController<TileObjectAnimation>, IForceMovableView{
        public void MoveToPosition(Vector3 worldPosition){
            Play(TileObjectAnimation.Move, new Move.Argument(){
                TargetPosition = worldPosition
            });
        }

        public void SetPosition(Vector3 targetPosition){
            transform.position = targetPosition;
        }

        public void BumpToUnsteppable(Direction direction){
            Play(TileObjectAnimation.Bump, new Bump.Argument(){
                Direction = direction.ToVector2Int()
            });
        }
    }
}