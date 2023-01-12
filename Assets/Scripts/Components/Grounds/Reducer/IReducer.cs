using Components.Effects;

namespace Components.Grounds.Reducer{
    public interface IReducer{
        public IEffectTemplate TakeElement(Ground ground, ElementType element);
    }
}