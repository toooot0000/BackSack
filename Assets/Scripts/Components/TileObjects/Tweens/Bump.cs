using UnityEngine;
using Utility.Animation;
using Utility.Animation.Tweens;
using Utility.Extensions;

namespace Components.TileObjects.Tweens{
    
    public class Bump: Tween, IAnimatorHandleInterrupt{
        
        public class Argument : IAnimatorArgumentTyped<Bump>{
            public Vector2Int Direction;
            public void SetUp(Bump animator){
                var direction = Direction;
                if (direction.magnitude.AlmostEquals(0)) return;
                animator._originPosition = animator.transform.position;
                var aligned = direction.AlignedDirection().ToVector2Int();
                animator._currentDirection = new Vector3(aligned.x, aligned.y, 0);
                animator.ResetTime();
            }
        }
        
        /// <summary>
        /// start from 0 and end at 0.
        /// </summary>
        public AnimationCurve curve;
        private Vector3 _originPosition;
        private Vector3 _currentDirection;
        public float maxDistance = 0.3f;

        protected override void OnTimerUpdate(float i){
            i = curve.Evaluate(i);
            transform.position = i * maxDistance * _currentDirection + _originPosition;
        }

        public void HandleInterrupt(IAnimator next){
            transform.position = _originPosition;
        }
    }
}