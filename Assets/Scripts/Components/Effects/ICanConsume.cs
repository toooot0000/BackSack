namespace Components.Effects{
    public interface ICanConsume<in TEffect>: IEffectConsumer where TEffect: IEffect{
        IEffect Consume(TEffect effect);
    }
}