using MVC;

namespace Components.Effects{

    public interface IEffect{
        IEffectConsumer Target{ set; get; }
        IController Source{ set; get; }
    }
}