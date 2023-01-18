using Components.Buffs.Effects;
using Components.Buffs.Triggers;
using Components.Damages;
using Components.Effects;

namespace Components.Buffs.Instances{
    
    public class BurningBuff: Buff, IOnTurnEnd{
        
        public IEffect OnTurnEnd(IBuffHolder holder){
            if (holder is not IDamageable damageable) return null;
            var ret = new MultiEffect(new IEffect[]{
                new Damage(null, damageable, 1, ElementType.Physic),
                new ChangeBuffEffect<BurningBuff>(holder, -1)
            });
            return ret;
        }

        public BurningBuff() : base(1){ }
    }
}