using Components.Ground.Reducer;

namespace Components.Ground.Instances{
    public class Explosion: IReducer{
        public GroundType TakeElement(ElementType element){
            return GroundType.Null;
        }
    }
}