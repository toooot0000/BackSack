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
}