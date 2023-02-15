
using Components.TileObjects.Tweens;
using UnityEngine;
using Utility.Animation;
using Utility.Extensions;

namespace Components.TileObjects.Movables{
    public class MovableView: AnimationController<TileObjectAnimation>, IMovableView{
        public virtual void MoveToPosition(Vector3 worldPosition){
            Play(TileObjectAnimation.Move, new Move.Argument(){
                TargetPosition = worldPosition
            });
        }

        public virtual void SetPosition(Vector3 targetPosition){
            transform.position = targetPosition;
        }

        public virtual void Destroy(){
            gameObject.SetActive(false);
        }

        public virtual void BumpToUnsteppable(Direction direction){
            Play(TileObjectAnimation.Bump, new Bump.Argument(){
                Direction = direction.ToVector2Int()
            });
        }
    }
}