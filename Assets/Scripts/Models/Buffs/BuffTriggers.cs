using System.Collections.Generic;

namespace Models.Buffs{
    public interface IBuffTrigger{ }

    public interface IBuffTriggerOnTurnBegin : IBuffTrigger{
        BuffEffectInfo OnTurnBegin(IBuffHolder holder);
    }

    public interface IBuffTriggerOnTurnEnd : IBuffTrigger{
        BuffEffectInfo OnTurnEnd(IBuffHolder holder);
    }

    public interface IBuffTriggerOnTakeDamage : IBuffTrigger{
        BuffEffectInfo OnTakeDamage(IBuffHolder holder);
    }

    public interface IBuffTriggerOnApplied : IBuffTrigger{
        BuffEffectInfo OnApplied(IBuffHolder holder);
    }

    public interface IBuffTriggerOnRemoved : IBuffTrigger{
        BuffEffectInfo OnRemoved(IBuffHolder holder);
    }

    public interface IBuffTriggerOnGetResistance : IBuffTrigger{
        BuffEffectInfo OnGetResistance(IBuffHolder holder);
    }
    
}