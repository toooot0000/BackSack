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
        Predicate<IController> Predicate{ get; }
        
        ElementType Element{ get; }
        public int LastTurn{ get; }
    }


    public class Attack : IAttack{
        public Attack(IAttacker attacker, Vector2Int[] relativeRange, IEffect effect, Predicate<IController> predicate, int lastTurn){
            Attacker = attacker;
            RelativeRange = relativeRange;
            Effect = effect;
            Predicate = predicate;
            LastTurn = lastTurn;
        }
        public IAttacker Attacker{ get; }
        public Vector2Int[] RelativeRange{ get; }
        public IEffect Effect{ get; }
        public Predicate<IController> Predicate{ get; }
        public ElementType Element => Effect.Element;
        public int LastTurn{ get; }
    }
}