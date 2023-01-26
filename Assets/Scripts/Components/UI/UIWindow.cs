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

        public void Open(){
            OnOpen();
        }
        
        /// <summary>
        /// ONLY entry to close a window. Even Manager would call this function.
        /// </summary>
        public void Close(){
            OnClose();
            Closed?.Invoke(this);
            Closed = null;
        }

        protected abstract void OnOpen();
        protected abstract void OnClose();

        public event UIWindowDelegate Closed;
    }
}