using MVC;

namespace Components.Damages{
    public class DamageBase{
        public int Point;
        public ElementType Element;
        
        public static int GetFinalPoint(DamageBase damage, IDamageable target){
            return damage.Point;
        }
    }
}