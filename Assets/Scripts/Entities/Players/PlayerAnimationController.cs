using Utility.Animation;

namespace Entities.Players{
    public enum PlayerAnimation{
        Jump
    }
    public class PlayerAnimationController : SubAnimationController<PlayerAnimation>{ }
}