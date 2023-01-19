using System;
using System.Collections;
using UnityEngine;
using Utility;

namespace UI{

    public interface IUISetUpOptions<in T> where T: UIWindow{
        void ApplyOptions(T window);
    }
    
    public abstract class UIWindow : MonoBehaviour{

        public static IEnumerator FadeIn(CanvasGroup canvasGroup, Action completion = null){
            var coroutine = TweenUtility.Lerp(0.2f,
                () => canvasGroup.alpha = 0,
                i => canvasGroup.alpha = i,
                completion
            );
            yield return coroutine();
        }

        public static IEnumerator FadeOut(CanvasGroup canvasGroup, Action completion = null){
            var coroutine = TweenUtility.Lerp(0.2f,
                () => canvasGroup.alpha = 1,
                i => canvasGroup.alpha = 1 - i,
                completion);
            yield return coroutine();
        }
        
        public virtual string Name => "Base";

        public virtual void Open(){
            OnOpen?.Invoke(this);
        }

        public virtual void Close(){
            OnClose?.Invoke(this);
        }

        public event UIWindowDelegate OnOpen;
        public event UIWindowDelegate OnClose;
    }
}