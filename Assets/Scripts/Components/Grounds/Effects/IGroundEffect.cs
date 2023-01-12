using Components.Effects;
using MVC;

namespace Components.Grounds.Effects{

    public interface IGroundEffect: IEffect{
        ElementType Element{ get; }
        int LastTurnNum{ get; }
    }

    public class GroundEffect : IGroundEffect{
        public GroundEffect(ElementType element, int lastTurnNum){
            Element = element;
            LastTurnNum = lastTurnNum;
        }
        public IEffectConsumer Target{ get; set; }
        public IController Source{ get; set; }
        public ElementType Element{ get; set; }
        public int LastTurnNum{ get; set; }
    }
}