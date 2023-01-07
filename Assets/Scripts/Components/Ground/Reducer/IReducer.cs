using Models;
using Models.Ground;

namespace Components.Ground.Reducer{
    public interface IReducer{
        public GroundType TakeElement(ElementType element);
    }
}