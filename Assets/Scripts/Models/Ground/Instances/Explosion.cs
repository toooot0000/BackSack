using Models.Ground.Reducer;

namespace Models.Ground.Instances{
    public class Explosion: IReducer{
        public GroundType TakeElement(ElementType element){
            return GroundType.Null;
        }
    }
}