using System;
using TMPro;
using UnityEngine;
using Utility.Animation.Tweens;

namespace Components.Damages.DamageNumbers{
    public class DamageNumber: Tween{
        private Damage _damage;
        public Damage Value{
            set{
                _damage = value;
                text.text = $"-{value.Point.ToString()}";
                text.alpha = 1;
                text.color = StartColor;
                transform.localPosition = Vector3.zero;
                Replay();
            }
        }
        public AnimationCurve floatingSpeedCurve;
        public TextMeshPro text;

        private Color StartColor => _damage.Type switch{
            ElementType.Physic => Color.red,
            ElementType.Fire => Color.yellow,
            ElementType.Poison => Color.green,
            ElementType.Earth => Color.grey,
            ElementType.Electric => Color.blue,
            ElementType.Real => Color.red,
            ElementType.Water => Color.cyan,
            ElementType.Wind => Color.white,
            _ => throw new ArgumentOutOfRangeException()
        };

        protected override void Start(){
            base.Start();
            text.alpha = 0;
        }

        protected override void OnTimerUpdate(float i){
            var spd = floatingSpeedCurve.Evaluate(i);
            transform.localPosition += Vector3.up * spd;
            text.alpha = 1 - i;
        }
    }
}