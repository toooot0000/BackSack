using Models.Damageable;

namespace Models.Buffs.BuffInstances{
    public class BurningBuff: Buff, IBuffTriggerOnTurnEnd{
        protected override string GetBuffName() => "burning";

        public void OnTurnEnd(IBuffHolder holder){
            if (holder is not IDamageable damageable) return;
            holder.TakeDamage(new Damage(){
                Point = 1,
                Type = ElementType.Fire
            });
            RemoveLayer(1);
        }
    }
}