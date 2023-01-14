using System.Collections;
using Components.Attacks;
using Components.Effects;
using UnityEngine;

namespace Components.Items.Animations{
    public abstract class ItemAnimator: MonoBehaviour, IAttackAnimator{
        public IEffect Result{ protected set; get; } = null;
        public abstract IEnumerator Play(IAttack attack);
    }
}