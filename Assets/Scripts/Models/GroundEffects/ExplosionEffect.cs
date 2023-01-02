namespace Models.GroundEffects{
    public class ExplosionEffect: IGroundEffect{
        public GroundEffectType TakeElement(ElementType element){
            return GroundEffectType.Null;
        }
    }
}