using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coroutines{
    public class CoroutineManager: MonoBehaviour{

        private readonly Queue<CoroutineData> _currentDispatch = new();
        private Coroutine _collectCoroutine = null;
        internal int FinishedNumber = 0;
        private int _targetNumber = 0;

        public CoroutineHandler SubmitAnimation(CoroutineData data){
            _collectCoroutine ??= StartCoroutine(StartCurrentDispatch());
            _currentDispatch.Enqueue(data);
            var ret = new CoroutineHandler(){
                Data = data
            };
            data.Handler = ret;
            return ret;
        }

        public bool CancelAnimation(CoroutineHandler handler){
            if (_collectCoroutine == null) return false;
            if (handler.IsCanceled || handler.IsFinished) return false;
            StopCoroutine(handler.Data.Coroutine);
            _targetNumber--;
            handler.IsCanceled = true;
            handler.Data = null;
            return true;
        }

        private void Update(){
            if (_collectCoroutine == null && _currentDispatch.Count > 0){
                _collectCoroutine = StartCoroutine(StartCurrentDispatch());
            }
        }

        private IEnumerator StartCurrentDispatch(){
            yield return new WaitForEndOfFrame();
            foreach (var data in _currentDispatch){
                data.Coroutine = StartCoroutine(data.ToCoroutine(this));
            }
            _targetNumber = _currentDispatch.Count;
            _currentDispatch.Clear();
            yield return new WaitUntil(() => FinishedNumber == _targetNumber);
            ClearCurrentDispatch();
        }

        private void ClearCurrentDispatch(){
            FinishedNumber = 0;
            _targetNumber = 0;
            _collectCoroutine = null;
        }
    }


    public class CoroutineData{
        private readonly Func<bool> _startPredict;
        private readonly Func<IEnumerator> _coroutineFunc;
        internal CoroutineHandler Handler;
        internal Coroutine Coroutine = null;
        public CoroutineData(Func<bool> startPredict, Func<IEnumerator> coroutineFunc){
            _startPredict = startPredict;
            _coroutineFunc = coroutineFunc;
        }

        public CoroutineData(Func<IEnumerator> coroutineFunc){
            _coroutineFunc = coroutineFunc;
            _startPredict = null;
        }

        internal IEnumerator ToCoroutine(CoroutineManager manager){
            if(_startPredict != null) yield return new WaitUntil(_startPredict);
            Handler.IsStarted = true;
            yield return _coroutineFunc();
            Handler.IsFinished = true;
            Handler.Data = null;
            manager.FinishedNumber++;
        }
    }

    public class CoroutineHandler{
        public bool IsStarted{ internal set; get; } = false;
        public bool IsFinished{ internal set; get; } = false;
        public bool IsCanceled{ internal set; get; } = false;
        internal CoroutineData Data = null;
    }
}