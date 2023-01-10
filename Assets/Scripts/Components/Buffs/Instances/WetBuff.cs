using Components.Buffs.Effects;
using Components.Buffs.Triggers;
using Components.Damages;
using Components.Effects;

namespace Components.Buffs.Instances{
    public class WetBuff: Buff, IOnTurnEnd, IOnConsumeDamage{
        protected override string GetBuffName() => "wet";


        public IEffect OnTurnEnd(IBuffHolder holder){
            return new ChangeBuffEffect<WetBuff>(holder, -1);
        }

        public IEffect OnConsumeDamageEffect(IBuffHolder holder, IDamageEffect effect){
            if (holder is not IDamageable damageable) return null;
            switch (effect.Element){
                case ElementType.Electric:
                    return new DamageEffect(null, damageable, new Damage(){
                        Point = Layer,
                        Type = ElementType.Electric
                    });
                case ElementType.Fire:
                    effect.Damage.Point = 0;
                    return new ChangeBuffEffect<WetBuff>(holder, -1);
                case ElementType.Wind:
                    return new ChangeBuffEffect<WetBuff>(holder, -Layer);
                
                case ElementType.Water:
                case ElementType.Earth:
                case ElementType.Poison:
                case ElementType.Physic:
                case ElementType.Real:
                default:
                    return null;
            }
        }
    }
}