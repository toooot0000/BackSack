using Components.Effects;

namespace Components.Damages{
    public interface IDamageEffect: IEffectTypedSource<IDamageable>, IEffectTypedTarget<IDamageable>{
        Damage Damage{ get; }
    }
    
    
    public class DamageEffect : IDamageEffect{
        public IDamageable Source{ get; }
        public IDamageable Target{ get; }
        public Damage Damage{ get; }
        
        public int DeductedHealthPoint = 0;
        public int DeductedDefendPoint = 0;
        public int DeductedShieldPoint = 0; // either 0 or 1
        
        public DamageEffect(IDamageable source, IDamageable target, Damage damage){
            Source = source;
            Target = target;
            Damage = damage;
        }
        public bool IsShielded => DeductedShieldPoint == 1;
        public bool IsDefended => DeductedDefendPoint > 0;
    }
}