using System;
using UnityEngine;
using Utility.Extensions;

namespace Utility.Animation.Tweens{

    public abstract class Tween: MonoBehaviour, IAnimator{
        public float totalTime;
        public bool repeat = false;
        private float _curTime = 0;
        public bool IsPaused{ get; private set; } = true;
        public bool IsReversing{ get; private set; } = false;

        public float Length => totalTime;

        protected virtual void Start(){
            ResetTime();
        }

        protected virtual void Update(){
            if (IsPaused) return;
            _curTime += IsReversing ? -Time.deltaTime : Time.deltaTime;
            _curTime = Mathf.Clamp(_curTime, 0, totalTime);
            OnTimerUpdate( Mathf.Clamp01(_curTime / totalTime));
            if(0 < _curTime && _curTime < totalTime)  return;
            if (!repeat){
                IsPaused = true;
                if(IsReversing){
                    OnReverseComplete();
                } else{
                    OnComplete();
                }
            } else {
                _curTime = IsReversing? totalTime : 0;
                OnRepeat();
            }
        }

        public void Pause(){
            IsPaused = true;
        }

        public void Play(){
            IsPaused = false;
            if(_curTime.AlmostEquals(0)){
                if(IsReversing) OnReverseComplete();
                else OnStart();
            };
        }

        public void Stop(){
            IsPaused = true;
            _curTime = 0;
        }

        public void Replay(){
            ResetTime();
            Play();
        }

        public void ResetTime(){
            _curTime = IsReversing? totalTime : 0;
        }

        public void Reverse(bool? val = null){
            if (val is { } b){
                IsReversing = b;
            } else{
                IsReversing = !IsReversing;
            }
        }

        public float CurrentTime => _curTime;

            /// <summary>
        /// If Tween is repeating, IsFinished is always false.
        /// </summary>
        public bool IsComplete => !repeat && !IsReversing && _curTime.AlmostEquals(totalTime);

        public bool IsReverseComplete => !repeat && IsReversing && _curTime.AlmostEquals(0);
        
        
        protected abstract void OnTimerUpdate(float i);
        protected virtual void OnComplete(){ }
        protected virtual void OnRepeat(){ }
        protected virtual void OnStart(){ }
        protected virtual void OnReverseStart(){ }
        protected virtual void OnReverseComplete(){ }
    }
}