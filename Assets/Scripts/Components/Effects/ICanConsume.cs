namespace Components.Effects{
    public interface ICanConsume<in TEffect>: IEffectConsumer where TEffect: IEffect{
        IEffectResult Consume(TEffect effect);
    }
}