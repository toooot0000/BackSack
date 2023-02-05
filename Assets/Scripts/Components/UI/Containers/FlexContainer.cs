using System;
using UnityEngine;
using Utility.Extensions;

namespace Components.UI.Containers{
    [RequireComponent(typeof(RectTransform))]
    public class FlexContainer: MonoBehaviour{
        [Serializable]
        public struct PaddingPairs{
            public Direction direction;
            public float padding;
        }
        public PaddingPairs[] paddings;

        private RectTransform _trans;

        private void Start(){
            _trans = (RectTransform)transform;
        }

        private bool _isSizeDirty = false;
        public void SetSizeDirty(){
            _isSizeDirty = true;
        }

        private void Update(){
            if (!_isSizeDirty) return;
            Resize();
        }

        public void Resize(){
            var curSize = Rect.zero;
            if (_trans.childCount == 0){
                SetSize(curSize.size);
                return;
            }

            var rect = _trans.GetChild(0).GetComponent<RectTransform>().rect;
            curSize = rect;

            for (var i = 1; i < _trans.childCount; i++){
                rect = _trans.GetChild(i).GetComponent<RectTransform>().rect;
                curSize.min = Vector2.Min(curSize.min, rect.min);
                curSize.max = Vector2.Min(curSize.max, rect.max);
            }

            var position = _trans.TransformPoint(curSize.position);
            
            SetSize(rect.size);
        }

        protected virtual void SetSize(Vector2 size){
            size = Vector2.Max(size, Vector2.zero);
            _trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.x);
            _trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.y);
        }
    }
}