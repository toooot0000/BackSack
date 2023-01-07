using UnityEngine;
using Utility.Animation;
using Utility.Animation.Tweens;

namespace Components.TileObjects.Tweens{
    public class Move: Tween, IAnimatorHandleInterrupt{
        public class Argument : IAnimatorArgumentTyped<Move>{
            public Vector3 TargetPosition;
            public void SetUp(Move animator){
                animator._startPosition = animator.transform.position;
                animator._targetPosition = TargetPosition;
                animator.ResetTime();
            }
        }
        
        public AnimationCurve curve;

        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        protected override void OnTimerUpdate(float i){
            i = curve.Evaluate(i);
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, i);
        }

        public void HandleInterrupt(IAnimator next){ 
            transform.position = _targetPosition;
        }
    }
}