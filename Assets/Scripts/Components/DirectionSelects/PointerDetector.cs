using System;
using Components.Players;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility.Extensions;

namespace Components.DirectionSelects{
    [RequireComponent(typeof(Collider2D))]
    public class PointerDetector: MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler{

        public event Action<Direction> DirectionConfirmed;
        public event Action<Direction> DirectionChanged;

        private Collider2D _collider2D;
        public Camera main;

        private Player Player => GameManager.Shared.player;

        private void Awake(){
            _collider2D = GetComponent<Collider2D>();
        }

        private void OnDisable(){
            _collider2D.enabled = false;
        }

        private void OnEnable(){
            _collider2D.enabled = true;
            _isDown = false;
        }

        private bool _isDown = false;

        public void OnPointerDown(PointerEventData eventData){
            _isDown = true;
            UpdateDirection(main.ScreenToWorldPoint(eventData.position));
        }

        public void OnPointerUp(PointerEventData eventData){
            if (!_isDown) return;
            UpdateDirection(main.ScreenToWorldPoint(eventData.position));
            DirectionConfirmed?.Invoke(_curDir);
        }

        private void UpdateDirection(Vector3 pointerWorldPosition){
            var playerPosition = Player.GetWorldPosition();
            var dir = pointerWorldPosition - playerPosition;
            var newDir = new Vector2(dir.x, dir.y).AlignedDirection();
            if (newDir != _curDir){
                DirectionChanged?.Invoke(newDir);
            }
            _curDir = newDir;
        }

        private Direction _curDir;

        public void OnPointerMove(PointerEventData eventData){
            if (!_isDown) return;
            UpdateDirection(main.ScreenToWorldPoint(eventData.position));
        }
    }
}