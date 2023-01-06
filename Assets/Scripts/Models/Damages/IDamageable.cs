using System.Collections.Generic;

namespace Models.Damages{
    public interface IDamageable{
        int HealthPoint{ set; get; }
        int ShieldPoint{ set; get; }
        int DefendPoint{ set; get; }
        Dictionary<ElementType, int> Resistances{ set; get; }
        void TakeDamage(Damage damage);
    }
}