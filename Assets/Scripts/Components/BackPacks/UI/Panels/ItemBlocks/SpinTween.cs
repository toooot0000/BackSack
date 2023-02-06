using UnityEngine;
using Utility.Animation.Tweens;

namespace Components.BackPacks.UI.Panels.ItemBlocks{
    public class SpinTween: Tween{
        public float targetAngle;
        public AnimationCurve curve;
        private float? _origin;
        protected override void OnStart(){
            if (_origin is{ } ori){
                var rotation = transform.rotation;
                transform.rotation = Quaternion.Euler(rotation.x, rotation.y, ori);
            }
            _origin = transform.rotation.z;
        }

        protected override void OnTimerUpdate(float i){
            i = curve.Evaluate(i);
            var cur = Mathf.Lerp(_origin!.Value, targetAngle, i);
            var rotation = transform.rotation;
            transform.rotation = Quaternion.Euler(rotation.x, rotation.y, cur);
        }
    }
}