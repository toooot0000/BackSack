using System.Collections.Generic;
using MVC;

namespace Components.Effects{

    public interface IEffect{
        IEffectConsumer Target{ get; }
        IController Source{ get; }
        public static IEffect MakeSideEffect(List<IEffect> result){
            return result.Count switch{
                0 => null,
                1 => result[0],
                _ => new MultiEffect(result.ToArray())
            };
        }
    }

    public interface IEffectTemplate{
        IEffectConsumer Target{ set; get; }
        IController Source{ set; get; }
        IEffect ToEffect();
    }
}