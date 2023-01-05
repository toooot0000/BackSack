using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Utility.Animation.Tweens;

namespace Utility.Animation{
    [Serializable]
    public struct TweenPair<T> where T: Enum{
        public T type;
        public Tween tween;
    }
    
    public interface IAnimator{
        float Length{ set; get; }
        public void Play();
    }

    public interface IAnimatorArgument<in T> where T : IAnimator{
        void SetUp(T animator);
    }

    public interface IAnimatorHandleInterrupt{
        void HandleInterrupt(IAnimator next);
    }

    public class WrappedAnimator : IAnimator{
        public float Length{ get; set; }
        public Animator UnderlyingAnimator;
        public string AnimationName;
        public void Play(){
            UnderlyingAnimator.Play(AnimationName);
        }
    }
    
    [RequireComponent(typeof(Animator))]
    public class AnimationController<T> : MonoBehaviour
        where T : Enum{
        public TweenPair<T>[] tweenPairs;
        private readonly Dictionary<T, IAnimator> _animator = new();
        private IAnimator _currentAnimator = null;

        private void Awake(){
            UpdateAnimatorData();
            UpdateTweenData();
        }

        private void UpdateAnimatorData(){
            var animator = GetComponent<Animator>();
            foreach (var clip in animator.runtimeAnimatorController.animationClips){
                try{
                    var type = EnumUtility.GetValue<T>(clip.name);
                    _animator[type] = new WrappedAnimator(){
                        UnderlyingAnimator = animator,
                        AnimationName = GetDescription(type),
                        Length = clip.length + 0.02f
                    };
                } catch{
                    Debug.Log($"Enum does not have the animation with the name of {clip.name}");
                }
            }
        }

        private void UpdateTweenData(){
            foreach (var tweenPair in tweenPairs){
                _animator[tweenPair.type] = tweenPair.tween;
            }
        }

        public void Play<TAnimator>(T anim, IAnimatorArgument<TAnimator> argument = null) where TAnimator : IAnimator{
            var animator = _animator[anim];
            if (animator is not TAnimator typed) return;
            if(_currentAnimator is IAnimatorHandleInterrupt handleInterrupt) handleInterrupt.HandleInterrupt(animator);
            _currentAnimator = animator;
            argument?.SetUp(typed);
            animator.Play();
        }

        public IEnumerator PlayAndWaitUntilComplete<TAnimator>(T anim, IAnimatorArgument<TAnimator> argument = null) where TAnimator : IAnimator{
            Play(anim, argument);
            yield return new WaitForSeconds(_animator[anim].Length);
        }

        private static string GetDescription(T value){
            var field = value.GetType().GetField(value.ToString());
            return Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is not DescriptionAttribute
                attribute
                ? value.ToString()
                : attribute.Description;
        }
    }
}