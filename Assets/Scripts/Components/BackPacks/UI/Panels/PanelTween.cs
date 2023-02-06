using System;
using Components.BackPacks.UI.Panels.BackupAreas;
using UnityEngine;
using Utility;
using Utility.Animation.Tweens;

namespace Components.BackPacks.UI.Panels{
    public class PanelTween: Tween{

        public RectTransform panelTrans;
        public Vector2 targetSize = new(600, 400);
        public AnimationCurve curve;
        public BackupArea backupArea;
        public CanvasGroup buttonGroup;
        private Vector2 _origin;

        protected override void OnStart(){
            _origin = panelTrans.rect.size;
            backupArea.canvasGroup.blocksRaycasts = false;
            backupArea.canvasGroup.interactable = false;
            buttonGroup.blocksRaycasts = false;
            buttonGroup.interactable = false;
        }

        protected override void OnComplete(){
            backupArea.canvasGroup.blocksRaycasts = true;
            backupArea.canvasGroup.interactable = true;
            buttonGroup.blocksRaycasts = false;
            buttonGroup.interactable = false;
        }

        protected override void OnReverseStart(){
            _origin = panelTrans.rect.size;
            backupArea.canvasGroup.blocksRaycasts = false;
            backupArea.canvasGroup.interactable = false;
            buttonGroup.blocksRaycasts = false;
            buttonGroup.interactable = false;
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
            backupArea.canvasGroup.alpha = i;
            buttonGroup.alpha = i;
        }
    }
}