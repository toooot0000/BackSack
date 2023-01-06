using Models.Buffs.Effects;
using Models.Buffs.Effects.Interfaces;
using Models.Buffs.Triggers;
using Models.Damages;

namespace Models.Buffs.Instances{

    public class BurningTurnEndEffect: IBuffEffect, IDamageEffect, IRemoveBuffEffect<BurningBuff>{
        public IDamageable Source => Damage.Source;
        public IDamageable Target{ get; set; }
        public Damage Damage{ get; set; }
        public IBuffHolder Holder{ get; set; }
        public int RemovedLayerNumber{ get; set; }
    }
    
    public class BurningBuff: Buff, IOnTurnEnd{
        protected override string GetBuffName() => "burning";

        public IBuffEffect OnTurnEnd(IBuffHolder holder){
            if (holder is not IDamageable damageable) return null;
            var ret = new BurningTurnEndEffect(){
                Holder = holder,
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