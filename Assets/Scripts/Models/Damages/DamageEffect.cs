using MVC;

namespace Models.Damages{
    public class DamageEffect : Model, IDamageEffect{
        public IDamageable Source => Damage.Source;
        public IDamageable Target{ set; get; }
        public int DeductedHealthPoint = 0;
        public int DeductedDefendPoint = 0;
        public int DeductedShieldPoint = 0; // either 0 or 1
        public bool IsShielded => DeductedShieldPoint == 1;
        public bool IsDefended => DeductedDefendPoint > 0;
        public Damage Damage{ set; get; }
    }
}