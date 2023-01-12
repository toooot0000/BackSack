using Components.Effects;
using Components.TileObjects.ForceMovables;
using MVC;
using UnityEngine;

namespace Components.TileObjects.Effects{
    public interface IForceMovement: IEffect {
        /// <summary>
        /// positive: pushing; negative: pulling
        /// </summary>
        int Force{ get; }
        /// <summary>
        /// Moving direction; set when searching enemy;
        /// </summary>
        Vector2Int Direction{ set; get; }
        /// <summary>
        /// Pulling or pushing?
        /// </summary>
        bool Pulling{ get; }
    }

    public class ForceMovement : IForceMovement{
        public ForceMovement(int force, bool pulling){
            Force = force;
            Pulling = pulling;
        }
        public int Force{ get; }
        
        public Vector2Int Direction{ get; set; }
        public bool Pulling{ get; }
        public IEffectConsumer Target{ set; get; } = null;
        public IController Source{ set; get; } = null;
    }
}