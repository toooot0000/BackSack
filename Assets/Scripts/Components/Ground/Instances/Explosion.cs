using Components.Ground.Reducer;
using Models;
using Models.Ground;

namespace Components.Ground.Instances{
    public class Explosion: IReducer{
        public GroundType TakeElement(ElementType element){
            return GroundType.Null;
        }
    }
}