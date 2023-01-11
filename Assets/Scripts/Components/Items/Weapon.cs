using System;
using System.Collections.Generic;
using Components.Effects;
using MVC;
using UnityEngine;

namespace Components.Items {
    public abstract class WeaponModel: ItemModel {
        public abstract IEnumerable<Vector2Int> AttackRange{ get; }
        public abstract IEffect Effect{ get; }
        public abstract Predicate<IController> Predicate{ get; }
        public abstract int LastTurn{ get; }
    }
}