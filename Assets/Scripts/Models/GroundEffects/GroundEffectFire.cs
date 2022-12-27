using MVC;

namespace Models.GroundEffects{
    public class GroundEffectFire: Model, IGroundEffect{
        public IGroundEffect TakeEffect<TIn>(TIn effect) where TIn : IGroundEffect{
            return this; // 不会发生变化
        }
    }
}