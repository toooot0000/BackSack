using System;
using UnityEngine;

namespace Components.BackPacks.UI.Panels.ItemBlocks{
    public class DragController: MonoBehaviour{
        private bool _isDragged = false;
        private Vector3 _prevPosition;
        private Vector3 _prevMousePosition;

        public void StartDrag(){
            _isDragged = true;
            _prevPosition = transform.position;
            _prevMousePosition = Input.mousePosition;
        }

        public void EndDrag(){
            _isDragged = false;
        }

        private void Update(){
            if (!_isDragged) return;
            var curMousePos = Input.mousePosition;
            var offset = curMousePos - _prevMousePosition;
            transform.position = _prevPosition + offset;
        }
    }
}