using System;
using Components.Attacks;
using Components.Buffs.BuffNames;
using Components.Damages;
using Components.Damages.DamageNumbers;
using Components.TileObjects.ForceMovables;
using Components.TileObjects.Tweens;

namespace Components.TileObjects.BattleObjects{
    public interface IBattleObjectView: IForceMovableView{
        void TakeDamage(IDamage damage);
        void AddBuff(string buffName);
        void Die();
    }
    
    public abstract class BattleObjectView : ForceMovableView, IBattleObjectView{
        public DamageNumber generator;
        public BuffNameDisplay buffNameDisplay;

        public virtual void TakeDamage(IDamage damage){
            generator.AddDamageNumber(damage, transform);
            Play<Damaged>();
        }
        public void AddBuff(string buffName){
            buffNameDisplay.AddBuffNameDisplay(buffName, true, transform);
        }
        public abstract void Die();

        public void RemoveBuff(string buffName){
            buffNameDisplay.AddBuffNameDisplay(buffName, false, transform);
        }
    }
}