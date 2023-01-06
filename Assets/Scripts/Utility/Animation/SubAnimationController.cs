using System;

namespace Utility.Animation{
    internal interface ISubAnimationController{
        
    }

    public class SubAnimationController<T> : AnimationController<T>, IAnimator where T: Enum{
        public class Argument : IAnimatorArgumentTyped<SubAnimationController<T>>{
            public readonly T Anim;
            public readonly IAnimatorArgumentNonTyped SubArg; 
            public Argument(T anim, IAnimatorArgumentNonTyped argument){
                Anim = anim;
                SubArg = argument;
            }

            public void SetUp(SubAnimationController<T> animator){
                animator._nextAnim = Anim;
                animator._argument = SubArg;
            }
        }
        public AnimationController parent = null;
        private T _nextAnim;
        private IAnimatorArgumentNonTyped _argument;

        private void Start(){
            if(parent != null) parent.SubControllers[typeof(T)] = this;
        }

        public float Length{ get; set; }
        public void Play(){
            var animator = Animators[_nextAnim];
            if (!_argument.SetUp(animator)) return;
            animator.Play();
        }
    }
}