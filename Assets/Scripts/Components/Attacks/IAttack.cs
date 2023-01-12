using System;
using Components.Damages;
using Components.Effects;
using MVC;
using UnityEngine;

namespace Components.Attacks{

    public interface IAttack {
        IAttacker Attacker{ get; }
        Vector2Int[] RelativeRange{ get; }
        IEffect Effect{ get; }
        Predicate<IEffectConsumer> Predicate{ get; }
    }


    public class Attack : IAttack{
        public Attack(
            IAttacker attacker, 
            Vector2Int[] relativeRange, 
            IEffect effect, 
            Predicate<IEffectConsumer> predicate
        ){
            Attacker = attacker;
            RelativeRange = relativeRange;
            Effect = effect;
            Predicate = predicate;
        }
        public IAttacker Attacker{ get; }
        public Vector2Int[] RelativeRange{ get; }
        public IEffect Effect{ get; }
        public Predicate<IEffectConsumer> Predicate{ get; }
    }
}