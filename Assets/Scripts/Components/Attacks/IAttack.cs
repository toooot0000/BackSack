using System;
using System.Collections.Generic;
using Components.Effects;
using Components.Grounds;
using Components.TileObjects;
using MVC;
using UnityEngine;

namespace Components.Attacks{

    public interface IAttack{
        IAttacker Attacker{ get; }
        Vector2Int[] TargetPositions{ get; }
        IEffectTemplate EffectTemplate{ get; }
        Predicate<ITileObject> TargetPredicate{ get; }
        int TargetNum{ get; }
        /// <summary>
        /// passEffect和动画中使用
        /// </summary>
        IEnumerable<ITileObject> Targets{ get; }
    }


    public class Attack : IAttack{
        public Attack(
            IAttacker attacker,
            Vector2Int[] relativeRange, 
            IEffectTemplate effect, 
            Predicate<ITileObject> predicate, 
            int targetNum, IEnumerable<ITileObject> targets){
            TargetPositions = relativeRange;
            EffectTemplate = effect;
            TargetPredicate = predicate;
            TargetNum = targetNum;
            Targets = targets;
            Attacker = attacker;
        }

        public IAttacker Attacker{ get; }
        public Vector2Int[] TargetPositions{ get; }
        public IEffectTemplate EffectTemplate{ get; }
        public Predicate<ITileObject> TargetPredicate{ get; }
        public int TargetNum{ get; }
        public IEnumerable<ITileObject> Targets{ get; }
    }
}