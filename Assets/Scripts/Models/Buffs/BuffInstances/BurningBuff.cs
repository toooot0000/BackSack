using Models.Damageable;
using Models.EffectInfo;

namespace Models.Buffs.BuffInstances{

    public class BurningTurnEndEffect: BuffEffectInfo, IDamageEffect, IRemoveBuffEffect<BurningBuff>{
        public IDamageable Source => Damage.Source;
        public IDamageable Target{ get; set; }
        public Damage Damage{ get; set; }
        public IBuffHolder BuffHolder{ get; set; }
        public int RemovedLayerNumber{ get; set; }
    }
    
    public class BurningBuff: Buff, IBuffTriggerOnTurnEnd{
        protected override string GetBuffName() => "burning";

        public BuffEffectInfo OnTurnEnd(IBuffHolder holder){
            if (holder is not IDamageable damageable) return null;
            var ret = new BurningTurnEndEffect(){
                BuffHolder = holder,
                Target = damageable,
                Damage = new Damage(){
                    Point = 1,
                    Source = null,
                    Type = ElementType.Fire
                },
                RemovedLayerNumber = 1
            };
            return ret;
        }
    }
}