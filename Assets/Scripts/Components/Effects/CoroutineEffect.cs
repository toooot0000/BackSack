using System;
using System.Collections;
using MVC;

namespace Components.Effects{
    public sealed class CoroutineEffect: IEffect{
        public IEffectConsumer Target{ get; } = null;
        public IController Source{ get; } = null;

        public Func<CoroutineEffect, IEnumerator> Coroutine;
        
        public IEffect Result;
        
        public CoroutineEffect(Func<CoroutineEffect, IEnumerator> coroutine){
            Coroutine = coroutine;
        }
    }
}