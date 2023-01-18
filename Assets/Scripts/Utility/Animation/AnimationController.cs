using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Utility.Animation.Tweens;

namespace Utility.Animation{

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
        protected IAnimator CurrentAnimator = null;
        
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

        public void Play<TEnum>(TEnum anim, IAnimatorArgumentNonTyped argument) where TEnum : Enum{
            var type = typeof(TEnum);
            if (!SubControllers.ContainsKey(type)) return;
            var sub = SubControllers[type];
            if (sub is not SubAnimationController<TEnum> typedSub) return;
            UpdateCurrentAnimator(typedSub);
            var wrappedArg = new SubAnimationController<TEnum>.Argument(anim, argument);
            wrappedArg.SetUp(typedSub);
            typedSub.Play();
        }
        
        public IEnumerator PlayAndWaitUntilComplete<TEnum>(TEnum anim, IAnimatorArgumentNonTyped argument) where TEnum : Enum{
            Play(anim, argument);
            yield return new WaitForSeconds(CurrentAnimator.Length);
        }

        public void Play<TAnimator>(T anim, IAnimatorArgumentTyped<TAnimator> argument = null) where TAnimator : IAnimator{
            var animator = Animators[anim];
            if (animator is not TAnimator typed) return;
            UpdateCurrentAnimator(animator);
            argument?.SetUp(typed);
            animator.Play();
        }
        public IEnumerator PlayAndWaitUntilComplete<TAnimator>(T anim, IAnimatorArgumentTyped<TAnimator> argument = null) where TAnimator : IAnimator{
            Play(anim, argument);
            yield return new WaitForSeconds(CurrentAnimator.Length);
        }

        public void Play<TAnimator>(IAnimatorArgumentTyped<TAnimator> arg = null) where TAnimator : Tween{
            try{
                var first = (TAnimator)Animators.Values.First(t => t is TAnimator);
                UpdateCurrentAnimator(first);
                arg?.SetUp(first);
                first.Play();
            } catch (Exception e){
                Debug.LogError($"Can't play animation of type {typeof(TAnimator)}");
            }
        }
        
        public IEnumerator PlayAndWaitUntilComplete<TAnimator>(IAnimatorArgumentTyped<TAnimator> arg = null) where TAnimator : Tween{
            Play(arg);
            yield return new WaitForSeconds(CurrentAnimator.Length);
        }

        protected void UpdateCurrentAnimator(IAnimator newCurrent){
            if(CurrentAnimator is IAnimatorHandleInterrupt handleInterrupt) handleInterrupt.HandleInterrupt(newCurrent);
            CurrentAnimator = newCurrent;
            
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