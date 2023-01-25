using System;
using System.Collections;
using UnityEngine;
using Utility;

namespace Components.UI{
    public abstract class UIWindow : MonoBehaviour{

        public static IEnumerator FadeIn(CanvasGroup canvasGroup, Action completion = null){
            var coroutine = TweenUtility.Lerp(UIManager.UITransitionTime,
                () => canvasGroup.alpha = 0,
                i => canvasGroup.alpha = i,
                completion
            );
            yield return coroutine();
        }

        public static IEnumerator FadeOut(CanvasGroup canvasGroup, Action completion = null){
            var coroutine = TweenUtility.Lerp(UIManager.UITransitionTime,
                () => canvasGroup.alpha = 1,
                i => canvasGroup.alpha = 1 - i,
                completion);
            yield return coroutine();
        }

        public virtual void Open(){
            Opened?.Invoke(this);
            OnOpen();
        }

        public virtual void Close(){
            Closed?.Invoke(this);
            OnClose();
        }

        protected abstract void OnOpen();
        protected abstract void OnClose();

        public event UIWindowDelegate Opened;
        public event UIWindowDelegate Closed;
    }
}