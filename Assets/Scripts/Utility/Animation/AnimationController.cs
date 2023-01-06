using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Utility.Animation.Tweens;

namespace Utility.Animation{

    // public interface IAnimationControllerUsingTweens<T> where T: Enum{
    //     [Serializable]
    //     public struct TweenPair{
    //         public T type;
    //         public Tween tween;
    //     }
    //     public TweenPair[] TweenPairs{ set; get; }
    //     public void Play<TAnimator>(T anim, IAnimatorArgument<TAnimator> argument = null) where TAnimator : IAnimator;
    //     public IEnumerator PlayAndWaitUntilComplete<TAnimator>(T anim, IAnimatorArgument<TAnimator> argument = null)
    //         where TAnimator : IAnimator;
    // }

    // public static class AnimationControllerUsingTweensExtension{
    //     public static void UpdateTweenData<T>(this AnimationController<>)
    // }

    public abstract class AnimationController:  MonoBehaviour{
        public readonly Dictionary<Type, AnimationController> SubControllers = new();
    }

    [RequireComponent(typeof(Animator))]
    public class AnimationController<T> : AnimationController
        where T : Enum{
        private class WrappedAnimator : IAnimator{
            public float Length{ get; set; }
            public Animator UnderlyingAnimator;
            public string AnimationName;
            public void Play(){
                UnderlyingAnimator.Play(AnimationName);
            }
        }
        
        [Serializable]
        public struct TweenPair{
            public T type;
            public Tween tween;
        }
        
        public TweenPair[] tweenPairs;
        protected readonly Dictionary<T, IAnimator> Animators = new();
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
                    Animators[type] = new WrappedAnimator(){
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
                Animators[tweenPair.type] = tweenPair.tween;
            }
        }

        public void Play<TEnum, TSub>(TEnum anim, IAnimatorArgumentNonTyped argument) where TEnum : Enum where TSub : SubAnimationController<TEnum>{
            var type = typeof(TEnum);
            if (!SubControllers.ContainsKey(type)) return;
            var sub = SubControllers[type];
            if (sub is not TSub typedSub) return;
            if(_currentAnimator is IAnimatorHandleInterrupt handleInterrupt) handleInterrupt.HandleInterrupt(typedSub);
            _currentAnimator = typedSub;
            var wrappedArg = new SubAnimationController<TEnum>.Argument(anim, argument);
            wrappedArg.SetUp(typedSub);
            typedSub.Play();
        }

        public void Play<TAnimator>(T anim, IAnimatorArgumentTyped<TAnimator> argument = null) where TAnimator : IAnimator{
            var animator = Animators[anim];
            if (animator is not TAnimator typed) return;
            if(_currentAnimator is IAnimatorHandleInterrupt handleInterrupt) handleInterrupt.HandleInterrupt(animator);
            _currentAnimator = animator;
            argument?.SetUp(typed);
            animator.Play();
        }

        public IEnumerator PlayAndWaitUntilComplete<TAnimator>(T anim, IAnimatorArgumentTyped<TAnimator> argument = null) where TAnimator : IAnimator{
            Play(anim, argument);
            yield return new WaitForSeconds(Animators[anim].Length);
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