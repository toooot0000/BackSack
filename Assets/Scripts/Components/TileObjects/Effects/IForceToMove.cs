using Components.Effects;
using Components.TileObjects.ForceMovable;
using UnityEngine;

namespace Components.TileObjects.Effects{
    public interface IForceMovement : IEffectTypedTarget<IForceMovable>{
        Vector2Int Force{ get; }
    }

    public class ForceMovement : IForceMovement{
        public ForceMovement(IForceMovable target, Vector2Int force){
            Force = force;
            Target = target;
        }
        public IForceMovable Target{ set; get; }
        public Vector2Int Force{ set; get; }
    }
}