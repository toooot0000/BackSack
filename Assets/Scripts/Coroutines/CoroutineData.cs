using System;
using System.Collections;
using UnityEngine;

namespace Coroutines{
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
}