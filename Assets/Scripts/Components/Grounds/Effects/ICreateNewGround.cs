using Components.Effects;

namespace Components.Grounds.Effects{
    public interface ICreateNewGround: IGroundEffect{
        GroundType GroundType{ get; }
    }
}