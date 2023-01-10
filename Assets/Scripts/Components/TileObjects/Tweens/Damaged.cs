using UnityEngine;
using Utility.Animation.Tweens;

namespace Components.TileObjects.Tweens{
    public class Damaged: Tween{
        public SpriteRenderer spriteRenderer;
        public AnimationCurve curve;

        private Color _newColor = Color.red;

        protected override void OnStart(){
            _newColor = Color.red;
            spriteRenderer.color = _newColor;
        }

        protected override void OnComplete(){
            ResetTime();
        }

        protected override void OnTimerUpdate(float i){
            i = curve.Evaluate(i);
            _newColor.a = 1 - i;
            spriteRenderer.color = _newColor;
        }
        
    }
}