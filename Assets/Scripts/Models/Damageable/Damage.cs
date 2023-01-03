using System;
using MVC;

namespace Models.Damageable{
    public class Damage: Model{
        public IDamageable Source;
        public int Point;
        public ElementType Type;
        
        public static int GetFinalPoint(Damage damage, IDamageable target){
            return damage.Point; // TODO
        }
        
        public static DamageEffectInfo GetDamageResolvedInfo(IDamageable target, Damage damage){
            var finalPoint = GetFinalPoint(damage, target);
            var deductedDp = Math.Min(target.DefendPoint, finalPoint);
            var deductedHp = finalPoint - deductedDp;
            var deductedSp = 0;
            if (target.ShieldPoint > 0){
                deductedDp = 0;
                deductedHp = 0;
                deductedSp = 1;
            }
            
            return new DamageEffectInfo(){
                Target = target,
                DeductedHealthPoint = deductedHp,
                DeductedShieldPoint = deductedSp,
                DeductedDefendPoint = deductedDp,
                Damage = damage
            };
        }
    }
}