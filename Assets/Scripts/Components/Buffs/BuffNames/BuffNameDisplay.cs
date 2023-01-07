using TMPro;
using UnityEngine;
using Utility.Animation.Tweens;

namespace Components.Buffs.BuffNames{
    public class BuffNameDisplay: Tween{
        public AnimationCurve floatingSpeedCurve;
        public TextMeshPro text;

        public void ShowBuffName(string buffName, bool add = true){
            text.text = $"{buffName}";
            text.richText = true;
            if (!add) text.text = $"<s>{buffName}<\\s>";
            text.alpha = 1;
            text.color = StartColor;
            transform.localPosition = Vector3.zero;
            Replay();
        }

        private Color StartColor => Color.black;

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