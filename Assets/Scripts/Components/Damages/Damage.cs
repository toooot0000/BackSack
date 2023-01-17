using MVC;

namespace Components.Damages{
    public class Damage{
        public int Point;
        public ElementType Element;
        
        public static int GetFinalPoint(Damage damage, IDamageable target){
            return damage.Point; // TODO
        }
        
        // public static DamageEffect GetDamageEffect(IDamageable target, Damage damage){
        //     var finalPoint = GetFinalPoint(damage, target);
        //     var deductedDp = Math.Min(target.DefendPoint, finalPoint);
        //     var deductedHp = finalPoint - deductedDp;
        //     var deductedSp = 0;
        //     if (target.ShieldPoint > 0){
        //         deductedDp = 0;
        //         deductedHp = 0;
        //         deductedSp = 1;
        //     }
        //     
        //     return new DamageEffect(){
        //         Target = target,
        //         DeductedHealthPoint = deductedHp,
        //         DeductedShieldPoint = deductedSp,
        //         DeductedDefendPoint = deductedDp,
        //         Damage = damage
        //     };
        // }
    }
}