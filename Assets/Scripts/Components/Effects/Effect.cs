using System.Collections.Generic;
using MVC;

namespace Components.Effects{

    public interface IEffect{
        IEffectConsumer Target{ get; }
        IController Source{ get; }
        /// <summary>
        /// NOTE: Will clear the result list!!!
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IEffect MakeSideEffect(List<IEffect> result){
            var ret = result.Count switch{
                0 => null,
                1 => result[0],
                _ => new MultiEffect(result.ToArray())
            };
            result.Clear();
            return ret;
        }
    }

    public interface IEffectTemplate{
        IEffectConsumer Target{ set; get; }
        IController Source{ set; get; }
        IEffect ToEffect();
    }
}