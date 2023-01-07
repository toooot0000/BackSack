﻿using Components.Buffs.Effects;
using Components.Buffs.Triggers;
using Components.Damages;
using Components.Effects;
using Models;

namespace Components.Buffs.Instances{
    
    public class BurningBuff: Buff, IOnTurnEnd{
        protected override string GetBuffName() => "burning";

        public IEffect OnTurnEnd(IBuffHolderModel holder){
            if (holder is not IDamageable damageable) return null;
            var ret = new MultiEffect(new IEffect[]{
                new DamageEffect(null, damageable, new Damage(){
                    Point = 1,
                    Type = ElementType.Fire
                }),
                new ChangeBuffEffect<BurningBuff>(holder, -1)
            });
            return ret;
        }
    }
}