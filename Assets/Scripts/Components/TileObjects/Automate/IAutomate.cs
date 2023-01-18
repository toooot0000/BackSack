using Components.Effects;
using Components.SelectMaps;
using MVC;

namespace Components.TileObjects.Automate{
    public interface IAutomate: IController{
        IEffect DoAction();
        void ShowIntention(SelectMap selectMap);
        bool IsActive{ get; }
    }
}