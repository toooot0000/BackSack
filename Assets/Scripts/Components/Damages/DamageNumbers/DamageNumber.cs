using System;
using Components.Gizmos.ObjectToasts;
using UnityEngine;

namespace Components.Damages.DamageNumbers{
    public class DamageNumber: MonoBehaviour{
        public ObjectToaster toaster;
        public void AddDamageNumber(IDamage damage, Transform parent){
            var startColor = StartColor(damage.Element);
            toaster.AddToast($"-{damage.Point.ToString()}", parent, new ToastOptions(){
                Start = startColor,
                End = startColor
            });
        }
        
        private Color StartColor(ElementType damage) => damage switch{
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
    }
}