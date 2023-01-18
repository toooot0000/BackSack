using Components.Effects;
using MVC;

namespace Components.Damages{
    public interface IHeal: IEffect{
        int Point{ get; }
    }
    
    public class Heal: IHeal, IEffectTemplate{
        public Heal(int point){
            Point = point;
        }
        public IEffectConsumer Target{ get; set; }
        public IController Source{ get; set; }
        public IEffect ToEffect() => this;

        public int Point{ get; }
    }
}