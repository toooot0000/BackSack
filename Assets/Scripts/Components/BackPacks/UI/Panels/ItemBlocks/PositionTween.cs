using System;
using UnityEngine;

namespace Components.BackPacks.UI.Panels.ItemBlocks{
    public class PositionTween: MonoBehaviour{

        private Vector3 _target; 
        public Vector3 Target{
            set{
                enabled = true;
                _target = value;
            }
            get => _target;
        }

        public AnimationCurve curve;
        public float maxRange = 300f;
        public float maxSpeed = 100f;
        public float minRange = 1f;


        public void Update(){
            var curPos = transform.position;
            var dir = Target - curPos;
            var curI = (Mathf.Clamp(dir.magnitude, minRange, maxRange) - minRange)/maxRange;
            var curSpd = curve.Evaluate(curI) * maxSpeed * dir.normalized;
            curPos += curSpd * Time.deltaTime;
            transform.position = curPos;
            if (curI == 0){
                transform.position = Target;
                enabled = false;
            }
        }
    }
}