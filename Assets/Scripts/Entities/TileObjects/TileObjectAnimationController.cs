using System.Collections;
using Entities.Players;
using Entities.TileObjects.Tweens;
using UnityEngine;
using Utility.Animation;
using Utility.Animation.Tweens;

namespace Entities.TileObjects{
    

    public enum TileObjectAnimation{
        Move,
        Bump,
    }
    
    public class TileObjectAnimationController: AnimationController<TileObjectAnimation>{
        public void MoveToPosition(Vector3 worldPosition){
            Debug.Log($"Move to: {worldPosition.ToString()}");
            Play(TileObjectAnimation.Move, new Move.Argument(){
                TargetPosition = worldPosition
            });
        }

        public IEnumerator MoveToPositionRoutine(Vector3 targetPosition){
            yield return PlayAndWaitUntilComplete(TileObjectAnimation.Move, new Move.Argument(){
                TargetPosition = targetPosition
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