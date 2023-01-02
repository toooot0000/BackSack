using System.Collections.Generic;

namespace Models.Buffs{
    public interface IBuffTrigger{ }

    public interface IBuffTriggerOnTurnBegin : IBuffTrigger{
        void OnTurnBegin(IBuffHolder holder);
    }

    public interface IBuffTriggerOnTurnEnd : IBuffTrigger{
        void OnTurnEnd(IBuffHolder holder);
    }

    public interface IBuffTriggerOnTakeDamage : IBuffTrigger{
        void OnTakeDamage(IBuffHolder holder);
    }

    public interface IBuffTriggerOnApplied : IBuffTrigger{
        void OnApplied(IBuffHolder holder);
    }

    public interface IBuffTriggerOnRemoved : IBuffTrigger{
        void OnRemoved(IBuffHolder holder);
    }

    public interface IBuffTriggerOnGetResistance : IBuffTrigger{
        void OnGetResistance(Dictionary<ElementType, int> originResistance);
    }
    
}