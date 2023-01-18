using Components.TileObjects.Tweens;
using UnityEngine;
using Utility.Animation;

namespace Components.TileObjects.ForceMovables{
    public interface IForceMovableView : ITileObjectView{
        
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

        public void BumpToUnsteppable(Vector2Int direction){
            Play(TileObjectAnimation.Bump, new Bump.Argument(){
                Direction = direction
            });
        }
    }
}