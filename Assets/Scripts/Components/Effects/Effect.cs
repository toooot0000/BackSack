using MVC;

namespace Components.Effects{

    public interface IEffect{
        IEffectConsumer Target{ get; }
        IController Source{ get; }
    }

    public interface IEffectTemplate{
        IEffectConsumer Target{ set; get; }
        IController Source{ set; get; }
        IEffect ToEffect();
    }
}