namespace Components.Effects{
    public interface IEffectResult{
        IEffect Effect{ get; }
        IEffectConsumer Consumer{ get; }
    }

    public class EffectResult: IEffectResult{
        public EffectResult(IEffect effect, IEffectConsumer consumer){
            Effect = effect;
            Consumer = consumer;
        }
        public IEffect Effect{ get; }
        public IEffectConsumer Consumer{ get; }
    }
}