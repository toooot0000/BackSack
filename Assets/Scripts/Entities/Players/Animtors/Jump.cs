using UnityEngine;
using Utility.Animation;
using Utility.Animation.Tweens;

namespace Entities.Players.Animtors{
    public class Jump: Tween{

        private float _height;
        
        public class Argument : IAnimatorArgumentTyped<Jump>, IAnimatorArgumentNonTyped{
            private readonly float _height;
            
            public Argument(float height){
                _height = height;
            }

            public void SetUp(Jump animator){
                animator._height = _height;
            }

            public bool SetUp(IAnimator animator){
                if (animator is not Jump jump) return false;
                SetUp(jump);
                return true;
            }
        }

        protected override void OnTimerUpdate(float i){
            Debug.Log("Jump!");
            Debug.Log($"{_height.ToString()}");
        }
    }
}