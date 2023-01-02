using System;
using System.Collections.Generic;
using MVC;

namespace Models.Damageable{
    public interface IDamageable{
        int HealthPoint{ set; get; }
        int ShieldPoint{ set; get; }
        int DefendPoint{ set; get; }
        Dictionary<ElementType, int> Resistances{ set; get; }
        void TakeDamage(Damage damage);
    }

    public class DamageResolvedInfo: Model{
        public IDamageable Source => Damage.Source;
        public IDamageable Target;
        public int DeductedHealthPoint = 0;
        public int DeductedDefendPoint = 0;
        public int DeductedShieldPoint = 0; // either 0 or 1
        public bool IsShielded => DeductedShieldPoint == 1;
        public bool IsDefended => DeductedDefendPoint > 0;
        public Damage Damage;
    }
}