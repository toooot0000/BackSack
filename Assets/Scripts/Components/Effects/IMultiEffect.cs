using System;
using MVC;

namespace Components.Effects{
    public sealed class MultiEffect: IEffect{
        public MultiEffect(IEffect[] effects){
            Effects = effects;
        }
        public IEffect[] Effects{ get; set; }

        /// <summary>
        /// Target is not meaningful here; Each sub effect can have its own target!
        /// </summary>
        public IEffectConsumer Target{ set; get; } = null;
        public IController Source{ set; get; } = null;
    }
}