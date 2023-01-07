namespace Components.Effects{
    public interface IEffectConsumer{
        IEffectResult[] Consume(IEffect effect);
    }
}