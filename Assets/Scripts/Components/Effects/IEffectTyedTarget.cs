using System.Collections.Generic;
using MVC;

namespace Components.Effects{
    public interface IEffectTypedTarget<out T>: IEffect where T: IController{
        T Target{ get; }
    }
}