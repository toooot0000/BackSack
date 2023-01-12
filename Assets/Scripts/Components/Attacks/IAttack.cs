using System;
using Components.Damages;
using Components.Effects;
using MVC;
using UnityEngine;

namespace Components.Attacks{

    public interface IAttack {
        IController Attacker{ get; }
        Vector2Int AttackerPosition{ get; }
        Vector2Int[] RelativeRange{ get; }
        IEffect Effect{ get; }
        Predicate<IEffectConsumer> Predicate{ get; }
        int TargetNum{ get; }
    }


    public class Attack : IAttack{
        public Attack(
            IController attacker, 
            Vector2Int attackerPosition,
            Vector2Int[] relativeRange, 
            IEffect effect, 
            Predicate<IEffectConsumer> predicate, 
            int targetNum){
            Attacker = attacker;
            RelativeRange = relativeRange;
            Effect = effect;
            Predicate = predicate;
            TargetNum = targetNum;
            AttackerPosition = attackerPosition;
        }
        public IController Attacker{ get; }
        public Vector2Int AttackerPosition{ get; }
        public Vector2Int[] RelativeRange{ get; }
        public IEffect Effect{ get; }
        public Predicate<IEffectConsumer> Predicate{ get; }
        public int TargetNum{ get; }
    }
}