using System.Collections.Generic;
using System.Linq;
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
        private ObjectToast _last = null;
        
        public void AddToast(string message, Transform parent, ToastOptions options = null){
            var newToast = _toasts.FirstNotActiveOrNew(Make);
            var newTrans = (newToast.transform as RectTransform)!;
            if (_last != null && (!_last.gameObject.activeSelf || _last == newToast)) _last = null;

            newTrans.SetParent(parent, false);
            newToast.Show(message,
                start: options?.Start ?? Color.black,
                end: options?.End ?? Color.black,
                prev: _last
            );
            _last = newToast;
        }

        private ObjectToast Make(){
            return Instantiate(toastPrefab).GetComponent<ObjectToast>();
        }
    }
}