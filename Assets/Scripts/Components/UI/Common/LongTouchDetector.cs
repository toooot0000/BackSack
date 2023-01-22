using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.UI.Common{
    public abstract class LongTouchDetector: MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler{
        public float longTouchTime = 0.5f;
        public Camera cmr;
        
        private float _curTime = 0;
        private bool _countDown = false;
        private Vector2 _mousePosition;
        
        private void Update(){
            if (!_countDown) return;
            _curTime -= Time.deltaTime;
            if (!(_curTime <= 0)) return;
            OnLongTouch();
            _countDown = false;
        }

        protected abstract void OnLongTouch();

        public void OnPointerUp(PointerEventData eventData){
            _countDown = false;
        }

        public void OnPointerDown(PointerEventData eventData){
            _curTime = longTouchTime;
            _countDown = true;
            _mousePosition = eventData.position;
        }

        public void OnPointerMove(PointerEventData eventData){
            if (_countDown) _countDown = false;
        }
    }
}