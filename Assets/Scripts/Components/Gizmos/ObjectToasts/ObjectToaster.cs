using System.Collections.Generic;
using UnityEngine;
using Utility.Extensions;

namespace Components.Gizmos.ObjectToasts{

    public class ToastOptions{
        public Color Start;
        public Color End;
    }
    
    public class ObjectToaster: MonoBehaviour{
        public GameObject toastPrefab;
        private readonly List<ObjectToast> _toasts = new();
        
        public void AddToast(string message, Transform parent, ToastOptions options = null){
            var newToast = _toasts.FirstNotActiveOrNew(Make);
            var newTrans = newToast.transform;
            
            _toasts.ForEach(t => {
                if(t.gameObject.activeSelf) t.AddOffset((newTrans as RectTransform)!.rect.height);
            });
            
            newTrans.SetParent(parent, false);
            newToast.Show(message,
                start: options?.Start ?? Color.black,
                end: options?.End ?? Color.black
            );
        }

        private ObjectToast Make(){
            return Instantiate(toastPrefab).GetComponent<ObjectToast>();
        }
    }
}