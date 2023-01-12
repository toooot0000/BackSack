using Components.Effects;
using MVC;

namespace Components.Damages{
    public interface IDamageEffect: IEffect{
        Damage Damage{ set; get; }
    }
    
    
    public class DamageEffect : IDamageEffect{
        public IEffectConsumer Target{ set; get; } = null;
        public IController Source{ set; get; } = null;
        public Damage Damage{ set; get; }
        
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
        public ElementType Element => Damage.Element;
    }
}