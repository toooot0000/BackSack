namespace Components.Effects{
    public interface IEffectConsumer{
        IEffect Consume(IEffect effect);
    }
}