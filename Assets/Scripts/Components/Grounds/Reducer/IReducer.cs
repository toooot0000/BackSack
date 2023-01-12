using Components.Effects;

namespace Components.Grounds.Reducer{
    public interface IReducer{
        public IEffect TakeElement(Ground ground, ElementType element);
    }
}