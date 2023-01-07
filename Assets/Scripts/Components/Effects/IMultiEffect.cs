namespace Components.Effects{
    public interface IMultiEffect: IEffect{
        IEffect[] Effects{ get; }
    }

    public class MultiEffect: IMultiEffect{
        public MultiEffect(IEffect[] effects){
            Effects = effects;
        }
        public IEffect[] Effects{ get; }
    }
}