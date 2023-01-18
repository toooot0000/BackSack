using Components.Buffs.Effects;
using Components.Buffs.Triggers;
using Components.Damages;
using Components.Effects;

namespace Components.Buffs.Instances{
    public class WetBuff: Buff, IOnTurnEnd, IBeforeConsumeDamage{


        public IEffect OnTurnEnd(IBuffHolder holder){
            return new ChangeBuffEffect<WetBuff>(holder, -1);
        }

        public IEffect BeforeConsumeDamage(IBuffHolder holder, IDamage effect){
            if (holder is not IDamageable damageable) return null;
            switch (effect.Element){
                case ElementType.Electric:
                    return new Damage(null, damageable, Layer, ElementType.Electric);
                case ElementType.Fire:
                    effect.Point = 0;
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

        public WetBuff() : base(2){ }
    }
}