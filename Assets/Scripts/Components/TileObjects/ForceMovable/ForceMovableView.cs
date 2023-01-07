using Components.TileObjects.Tweens;
using MVC;
using UnityEngine;
using Utility.Animation;

namespace Components.TileObjects.ForceMovable{
    

    public enum TileObjectAnimation{
        Move,
        Bump,
        Damaged,
    }
    
    public class ForceMovableView: AnimationController<TileObjectAnimation>, ITileObjectView{
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