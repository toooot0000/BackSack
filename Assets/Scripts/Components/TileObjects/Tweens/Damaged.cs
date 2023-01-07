using UnityEngine;
using Utility.Animation.Tweens;

namespace Components.TileObjects.Tweens{
    public class Damaged: Tween{
        public SpriteRenderer spriteRenderer;
        public AnimationCurve curve;
        protected override void OnTimerUpdate(float i){
            i = curve.Evaluate(i);
            spriteRenderer.color = Color.Lerp(Color.red, Color.white, i);
        }
    }
}