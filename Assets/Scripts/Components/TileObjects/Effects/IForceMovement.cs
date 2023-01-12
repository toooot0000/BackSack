using Components.Effects;
using Components.TileObjects.ForceMovables;
using MVC;
using UnityEngine;

namespace Components.TileObjects.Effects{
    public interface IForceMovement: IEffect {
        Vector2Int Force{ get; }
    }

    public class ForceMovement : IForceMovement{
        public ForceMovement(Vector2Int force){
            Force = force;
        }
        public Vector2Int Force{ set; get; }
        public IEffectConsumer Target{ set; get; } = null;
        public IController Source{ set; get; } = null;
    }
}