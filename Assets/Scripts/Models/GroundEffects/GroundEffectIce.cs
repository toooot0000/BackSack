using MVC;

namespace Models.GroundEffects{
    public class GroundEffectIce: Model, IGroundEffect{
        public IGroundEffect TakeEffect<TIn>(TIn effect) where TIn : IGroundEffect{
            return this;
        }
    }
}