using Components.Effects;
using MVC;

namespace Components.TileObjects.Automate{
    public interface IAutomate: IController{
        IEffect DoAction();
    }
}