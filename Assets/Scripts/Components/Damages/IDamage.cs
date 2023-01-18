using Components.Effects;
using MVC;

namespace Components.Damages{
    public interface IDamage: IEffect{
        int Point{ set; get; }
        ElementType Element{ get; }
    } 
    
    
    public class Damage : IDamage, IEffectTemplate{
        public IEffectConsumer Target{ set; get; } = null;
        public IController Source{ set; get; } = null;
        public IEffect ToEffect() => this;
        
        public Damage(IController source, IEffectConsumer target, int point, ElementType element){
            Source = source;
            Target = target;
            Point = point;
            Element = element;
        }
        
        public int Point{ set; get; }
        public ElementType Element{ get; }
    }
    
    
}