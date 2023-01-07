using Utility.Animation;

namespace Components.Players{
    public enum PlayerAnimation{
        Jump
    }
    public class PlayerAnimationController : SubAnimationController<PlayerAnimation>{ }
}