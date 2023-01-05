using System.Collections.Generic;

namespace Models.Buffs{
    public interface IBuffHolder{
        List<Buff> Buffs{ set; get; }
    }
}