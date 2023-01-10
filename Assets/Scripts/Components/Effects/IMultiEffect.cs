using MVC;

namespace Components.Effects{
    public interface IMultiEffect: IEffect{
        IEffect[] Effects{ get; }
    }

    public class MultiEffect: IMultiEffect{
        public MultiEffect(IEffect[] effects, ElementType type = ElementType.Physic){
            Effects = effects;
            _type = type;
        }
        public IEffect[] Effects{ get; }

        private ElementType? _type = null;

        public IController Target{ set; get; } = null;
        public IController Source{ set; get; } = null;

        public ElementType Element{
            get{
                if (_type != null) return _type.Value;
                if (Effects.Length > 0) return Effects[0].Element;
                return ElementType.Physic;
            }
        }
    }
}