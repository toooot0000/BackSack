using System;
using UnityEngine;
using Utility;
using Utility.Animation.Tweens;

namespace Components.BackPacks.UI.Panels{
    public class PanelTween: Tween{

        public RectTransform panelTrans;
        public Vector2 targetSize = new(600, 400);
        public AnimationCurve curve;
        private Vector2 _origin;

        protected override void OnStart(){
            _origin = panelTrans.rect.size;
        }

        protected override void OnReverseStart(){
            _origin = panelTrans.rect.size;
        }

        public void Play(Action callback){
            Play();
            StartCoroutine(CoroutineUtility.Delayed(totalTime, callback));
        }

        protected override void OnTimerUpdate(float i){
            i = curve.Evaluate(i);
            var curSize = Vector2.Lerp(_origin, targetSize, i);
            panelTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, curSize.x);
            panelTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, curSize.y);
        }
    }
}