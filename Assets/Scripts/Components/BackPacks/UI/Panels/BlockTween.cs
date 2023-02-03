using System;
using UnityEngine;
using Utility.Animation.Tweens;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels{
    public class BlockTween: Tween{
        
        public Direction TargetDir;
        public Func<Vector3> TargetPosGenerator = null;

        public void SetUpAndPlay(Direction direction, Func<Vector3> positionGenerator){
            TargetDir = direction;
            TargetPosGenerator = positionGenerator;
            Play();
        }

        protected override void OnTimerUpdate(float i){
            var pos = TargetPosGenerator();
            
        }
    }
}