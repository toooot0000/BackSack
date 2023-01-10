using System.Collections.Generic;
using Components.Buffs.Effects;
using Components.Effects;
using MVC;

namespace Components.Buffs{
    // public interface IBuffHolderModel: IModel{
    //     List<Buff> Buffs{ set; get; }
    // }

    public interface IBuffHolder : IController, ICanConsume<IBuffEffect>{
        List<Buff> Buffs{ set; get; }
    }
}