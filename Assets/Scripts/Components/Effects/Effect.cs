using MVC;

namespace Components.Effects{

    public interface IEffect{
        IController Target{ set; get; }
        IController Source{ set; get; }
        ElementType Element{ get; }
    }
}