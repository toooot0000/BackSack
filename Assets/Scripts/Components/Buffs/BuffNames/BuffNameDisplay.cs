using TMPro;
using UnityEngine;
using Utility.Animation.Tweens;

namespace Components.Buffs.BuffNames{
    public class BuffNameDisplay: Tween{
        public AnimationCurve floatingSpeedCurve;
        public TextMeshPro text;
        public float maxSpd = 0.1f;

        public void ShowBuffName(string buffName, bool add = true){
            text.text = $"{buffName}";
            text.richText = true;
            if (!add) text.text = $"<s>{buffName}</s>";
            text.alpha = 1;
            text.color = StartColor;
            transform.localPosition = Vector3.zero;
            Replay();
        }

        private Color StartColor => Color.red;

        protected override void OnStart(){
            text.alpha = 0;
        }

        protected override void OnTimerUpdate(float i){
            var spd = floatingSpeedCurve.Evaluate(1 - i);
            transform.localPosition += Vector3.up * (spd * maxSpd);
            text.alpha = 1 - spd;
        }

        protected override void OnComplete(){
            gameObject.SetActive(false);
        }
    }
}