namespace Components.Effects{
    public interface IMultiEffectResult: IEffectResult{
        IEffectResult[] Results{ get; } 
    }

    public class MultiEffectResult : IMultiEffectResult{
        public MultiEffectResult(IMultiEffect effect, IEffectConsumer consumer, IEffectResult[] results){
            Effect = effect;
            Consumer = consumer;
            Results = results;
        }
        
        public IEffect Effect{ get; }
        public IEffectConsumer Consumer{ get; }
        public IEffectResult[] Results{ get; }
    }
}