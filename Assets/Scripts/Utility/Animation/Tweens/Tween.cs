using System;
using UnityEngine;
using Utility.Extensions;

namespace Utility.Animation.Tweens{

    public abstract class Tween: MonoBehaviour, IAnimator{

        public event Action OnRepeat;
        public event Action OnFinish;
        public float totalTime;
        public bool repeat = false;
        private float _curTime = 0;
        public bool IsPaused{ get; private set; } = true;
        
        public float Length{
            get => totalTime;
            set => totalTime = value;
        }


        protected virtual void Start(){
            _curTime = 0;
        }

        protected virtual void Update(){
            if (IsPaused) return;
            _curTime += Time.deltaTime;
            var i = _curTime / totalTime;
            OnTimerUpdate(i);
            if (_curTime.AlmostEquals(totalTime) || _curTime > totalTime){
                if (!repeat){
                    IsPaused = true;
                    OnFinish?.Invoke();
                }
                else{
                    _curTime = 0;
                    OnRepeat?.Invoke();
                }
            }
        }

        public void Pause(){
            IsPaused = true;
        }

        public void Play(){
            IsPaused = false;
        }

        public void Stop(){
            IsPaused = true;
            _curTime = 0;
        }

        public void Replay(){
            IsPaused = false;
            _curTime = 0;
        }

        public void ResetTime(){
            _curTime = 0;
        }
        
        /// <summary>
        /// If Tween is repeating, IsFinished is always false.
        /// </summary>
        public bool IsFinished => !repeat && _curTime.AlmostEquals(totalTime);

        protected abstract void OnTimerUpdate(float i);
    }
}