using Components.Attacks;
using Components.Buffs.BuffNames;
using Components.Damages;
using Components.Damages.DamageNumbers;
using Components.TileObjects.ForceMovables;
using Components.TileObjects.Tweens;

namespace Components.TileObjects.BattleObjects{
    public interface IBattleObjectView: IForceMovableView{
        void TakeDamage(Damage damage);
        void AddBuff(string buffName);
        void Attack(IAttack attack);
        void Die();
    }
    
    public abstract class BattleObjectView : ForceMovableView, IBattleObjectView{
        public DamageNumberGenerator generator;
        public BuffNameDisplayGenerator buffNameDisplayGenerator;

        public virtual void TakeDamage(Damage damage){
            generator.AddDamageNumber(damage, transform);
            Play<Damaged>();
        }

        public void AddBuff(string buffName){
            buffNameDisplayGenerator.AddBuffNameDisplay(buffName, true, transform);
        }

        public abstract void Attack(IAttack attack);

        public abstract void Die();

        public void RemoveBuff(string buffName){
            buffNameDisplayGenerator.AddBuffNameDisplay(buffName, false, transform);
        }
    }
}