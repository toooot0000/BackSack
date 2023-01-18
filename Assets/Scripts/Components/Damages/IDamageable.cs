using System.Collections.Generic;
using Components.Effects;
using MVC;

namespace Components.Damages{
    public interface IDamageableModel: IModel{
        int HealthPoint{ set; get; }
        int ShieldPoint{ set; get; }
        int DefendPoint{ set; get; }
    }

    public interface IDamageable : IController, ICanConsume<IDamage>{
        IEffect Die();
    }
}