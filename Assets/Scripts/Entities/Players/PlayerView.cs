using System.Collections;
using Entities.TileObjects;
using Models.TileObjects;
using MVC;
using UnityEngine;
using Utility.Animation;

namespace Entities.Players{

    public enum PlayerAnimation{
        Move,
        Bump,
        Attack,
        BeingAttacked
    }

    public class PlayerView: AnimationController<PlayerAnimation>, IViewWithType<Player>{
        
        public void MoveToPosition(Vector3 worldPosition){
            Play(PlayerAnimation.Move, new TileObjectMover.Argument(){
                TargetPosition = worldPosition
            });
        }

        public IEnumerator MoveToPositionRoutine(Vector3 targetPosition){
            yield return PlayAndWaitUntilComplete(PlayerAnimation.Move, new TileObjectMover.Argument(){
                TargetPosition = targetPosition
            });
        }

        public void SetPosition(Vector3 targetPosition){
            transform.position = targetPosition;
        }

        public void BumpToUnsteppable(Vector2Int direction){
            Play(PlayerAnimation.Bump, new TileObjectBumper.Argument(){
                Direction = direction
            });
        }
    }
}