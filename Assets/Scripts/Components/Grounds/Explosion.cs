using Components.Damages;
using Components.Effects;
using MVC;

namespace Components.Grounds{
    public class Explosion : IDamage, IEffectTemplate{
        public Explosion(){
            Point = 5;
            Element = ElementType.Fire;
        }
        public IEffectConsumer Target{ get; set; }
        public IController Source{ get; set; }
        public IEffect ToEffect() => this;

        public int Point{ set; get; }
        public ElementType Element{ get; }
    }
}