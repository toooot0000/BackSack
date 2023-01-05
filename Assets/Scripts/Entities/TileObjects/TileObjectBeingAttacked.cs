using UnityEngine;
using Utility.Animation.Tweens;

namespace Entities.TileObjects{
    public class TileObjectBeingAttacked: Tween{
        public AnimationCurve curve;
        public TileObjectAttack attack;


        protected override void OnTimerUpdate(float i){
            // TODO
        }
    }
}