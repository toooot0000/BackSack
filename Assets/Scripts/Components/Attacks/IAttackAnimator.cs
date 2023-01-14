using System;
using System.Collections;
using Components.Effects;

namespace Components.Attacks{
    public interface IAttackAnimator{
        public IEffect Result{ get; }
        IEnumerator Play(IAttack attack);
    }
}