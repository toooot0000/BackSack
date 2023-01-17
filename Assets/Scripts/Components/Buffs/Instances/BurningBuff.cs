using Components.Buffs.Effects;
using Components.Buffs.Triggers;
using Components.Damages;
using Components.Effects;

namespace Components.Buffs.Instances{
    
    public class BurningBuff: Buff, IOnTurnEnd{
        
        public IEffect OnTurnEnd(IBuffHolder holder){
            if (holder is not IDamageable damageable) return null;
            var ret = new MultiEffect(new IEffect[]{
                new DamageEffect(null, damageable, new Damage(){
                    Point = 1,
                    Element = ElementType.Fire
                }),
                new ChangeBuffEffect<BurningBuff>(holder, -1)
            });
            return ret;
        }

        public BurningBuff() : base(1){ }
    }
}