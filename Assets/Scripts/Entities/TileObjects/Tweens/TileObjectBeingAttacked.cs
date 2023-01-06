using UnityEngine;
using Utility.Animation.Tweens;

namespace Entities.TileObjects.Tweens{
    public class TileObjectBeingAttacked: Tween{
        public AnimationCurve curve;
        public TileObjectAttack attack;


        protected override void OnTimerUpdate(float i){
            // TODO
        }
    }
}