using System.Collections.Generic;
using Components.Effects;
using MVC;

namespace Components.Damages{
    public interface IDamageable : IController, ICanConsume<IDamage>{
        int HealthLimit{ set; get; }
        int HealthPoint{ set; get; }
        int ShieldPoint{ set; get; }
        int DefendPoint{ set; get; }
        IEffect Die();
    }
}