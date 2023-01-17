using Components.Effects;
using Components.Items.Effects;
using MVC;

namespace Components.Items{
    public interface IItemHolder: IController, ICanConsume<IItemChange>{ }
}