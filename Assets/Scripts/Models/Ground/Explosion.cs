namespace Models.Ground{
    public class Explosion: IGround{
        public GroundType TakeElement(ElementType element){
            return GroundType.Null;
        }
    }
}