using System;
using System.Collections;
using Components.Attacks;
using Components.Buffs.BuffNames;
using Components.Damages;
using Components.Damages.DamageNumbers;
using Components.TileObjects.Movables;
using Components.TileObjects.Tweens;

namespace Components.TileObjects.BattleObjects{
    public abstract class BattleObjectView : MovableView, IBattleObjectView{
        public DamageNumber generator;
        public BuffNameDisplay buffNameDisplay;

        public virtual void TakeDamage(IDamage damage){
            generator.AddDamageNumber(damage, transform);
            Play<Damaged>();
        }
        public void AddBuff(string buffName){
            buffNameDisplay.AddBuffNameDisplay(buffName, true, transform);
        }

        public virtual IEnumerator Die(){
            yield return PlayAndWaitUntilComplete<Die>();
            Destroy();
        }

        public void RemoveBuff(string buffName){
            buffNameDisplay.AddBuffNameDisplay(buffName, false, transform);
        }
    }
}