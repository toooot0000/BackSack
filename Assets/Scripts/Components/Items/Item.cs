using System;
using System.Collections.Generic;
using Components.Effects;
using MVC;
using UnityEngine;

namespace Components.Items{
    public abstract class ItemModel: Model{
        public static readonly Vector2Int DefaultDirection = Vector2Int.right;
        
        public Vector2Int[] TakeUpRange;
        public Vector2Int BackpackPosition;
        public string IconPath;
        public Vector2Int RotateDirection = Vector2Int.up;

        public abstract IEnumerable<Vector2Int> AttackRange{ get; }
        public abstract IEffect Effect{ get; }
        public abstract Predicate<IController> Predicate{ get; }
        public abstract int LastTurn{ get; }
    }

}