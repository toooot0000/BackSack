using TMPro;
using UnityEngine;
using Utility.Animation.Tweens;

namespace Components.Gizmos.ObjectToasts{
    public class ObjectToast: Tween{
        public AnimationCurve floatingCurve;
        public AnimationCurve alphaCurve;
        public AnimationCurve colorCurve;
        public TextMeshPro text;
        public float initDistance = 1f;

        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private Color _startColor;
        private Color _endColor;
        private ObjectToast _prev;

        public float TargetPosition => _prev == null || !_prev.gameObject.activeSelf
            ? initDistance
            : _prev.transform.localPosition.y + ((RectTransform)_prev.transform).sizeDelta.y; 
        
        public void Show(string message, Color start, Color end, ObjectToast prev = null){
            var position = transform.position;
            _startPosition = new Vector3(0, 0, position.z);
            _endPosition = new Vector3(0, initDistance, position.z);
            _startColor = start;
            _endColor = end;
            _prev = prev;
            text.text = message;
            Replay();
        }

        public void AddOffset(float offset){
            _endPosition.y += offset;
        }

        protected override void OnStart(){
            text.alpha = 0;
            text.richText = true;
        }

        protected override void OnTimerUpdate(float i){
            _endPosition.y = TargetPosition;
            transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, floatingCurve.Evaluate(i));
            text.color = Color.Lerp(_startColor, _endColor, colorCurve.Evaluate(i));
            text.alpha = alphaCurve.Evaluate(i);
        }

        protected override void OnComplete(){
            gameObject.SetActive(false);
        }
    }   
}
