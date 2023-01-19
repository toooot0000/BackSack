using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.Stages{
    
    [RequireComponent(typeof(Collider2D))]
    public class ClickHandler: MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
        public float longTouchTime = 0.5f;
        public Stage stage;
        public Action<Vector2Int> Action = null;
        public Camera cmr;
        
        private float _curTime = 0;
        private bool _countDown = false;
        private Vector2 _mousePosition;
        
        private void Update(){
            if (!_countDown) return;
            _curTime -= Time.deltaTime;
            if (!(_curTime <= 0)) return;
            Action?.Invoke(GetStagePosition());
            _countDown = false;
        }

        private Vector2Int GetStagePosition(){
            var worldPosition = cmr!.ScreenToWorldPoint(_mousePosition);
            return stage.WorldPositionToStagePosition(worldPosition);
        }
        
        public void OnPointerUp(PointerEventData eventData){
            _countDown = false;
        }

        public void OnPointerDown(PointerEventData eventData){
            _curTime = longTouchTime;
            _countDown = true;
            _mousePosition = eventData.position;
        }
    }
}