using Components.Buffs.BuffNames;
using Components.Damages;
using Components.Damages.DamageNumbers;
using Components.Players.Animtors;
using Components.TileObjects;
using Components.TileObjects.ForceMovable;
using Components.TileObjects.Tweens;
using MVC;

namespace Components.Players{

    public class PlayerView: ForceMovableView{
        public DamageNumberGenerator generator;
        public BuffNameDisplayGenerator buffNameDisplayGenerator;
        public void Jump(){
            Play(PlayerAnimation.Jump, new Jump.Argument(0));
        }

        public void TakeDamage(Damage damage){
            generator.AddDamageNumber(damage, transform);
            Play<Damaged>();
        }

        public void AddBuff(string buffName){
            buffNameDisplayGenerator.AddBuffNameDisplay(buffName, true, transform);
        }

        public void RemoveBuff(string buffName){
            buffNameDisplayGenerator.AddBuffNameDisplay(buffName, false, transform);
        }
    }
}