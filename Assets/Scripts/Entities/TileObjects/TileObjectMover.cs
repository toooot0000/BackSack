using System;
using UnityEngine;
using Utility.Animation;
using Utility.Animation.Tweens;
using Utility.Extensions;

namespace Entities.TileObjects{
    public class TileObjectMover: Tween, IAnimatorHandleInterrupt{
        public class Argument : IAnimatorArgument<TileObjectMover>{
            public Vector3 TargetPosition;
            public void SetUp(TileObjectMover animator){
                if(animator.TargetPosition != null){
                    animator.transform.position = animator.TargetPosition.Value;
                }
                animator.startPosition = animator.transform.position;
                animator.TargetPosition = TargetPosition;
                animator.ResetTime();
            }
        }
        
        public AnimationCurve curve;
        [HideInInspector]
        public Vector3 startPosition;
        
        public Vector3? TargetPosition = null;
        protected override void OnTimerUpdate(float i){
            i = curve.Evaluate(i);
            transform.position = Vector3.Lerp(startPosition, TargetPosition!.Value, i);
        }

        public void HandleInterrupt(IAnimator next){
            if (TargetPosition != null) transform.position = TargetPosition.Value;
        }
    }
}