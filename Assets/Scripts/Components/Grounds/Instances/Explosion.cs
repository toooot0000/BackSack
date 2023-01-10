using Components.Grounds.Reducer;

namespace Components.Grounds.Instances{
    public class Explosion: IReducer{
        public GroundType TakeElement(ElementType element){
            return GroundType.Null;
        }
    }
}