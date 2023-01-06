using TMPro;
using UnityEngine;
using Utility.Animation.Tweens;

namespace Entities.DamageNumber{
    public class DamageNumber: Tween{
        public int Value{
            set{
                text.text = $"{value.ToString()}";
                text.alpha = 1;
                transform.localPosition = Vector3.zero;
                Replay();
            }
        }
        public AnimationCurve floatingSpeedCurve;
        public TextMeshProUGUI text;
        public Color startColor = Color.red;

        protected override void Start(){
            base.Start();
            text.alpha = 0;
        }

        protected override void OnTimerUpdate(float i){
            var spd = floatingSpeedCurve.Evaluate(i);
            transform.localPosition += Vector3.up * spd;
            text.alpha = 1 - i;
        }
    }
}