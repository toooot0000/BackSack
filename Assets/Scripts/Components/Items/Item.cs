using System;
using System.Collections.Generic;
using Components.Attacks;
using Components.Effects;
using Components.Items.Animations;
using MVC;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Components.Items{
    public abstract class ItemModel: Model{
        public static readonly Vector2Int DefaultDirection = Vector2Int.right;
        public Vector2Int[] TakeUpRange;
        public Vector2Int BackpackPosition;
        public string IconPath;
        public Vector2Int RotateDirection = Vector2Int.up;
        public abstract IEffectTemplate EffectTemplate{ get; }
        public abstract string AnimatorPrefabPath{ get; }
        
        public abstract IEnumerable<Vector2Int> Range{ get; }
        public abstract Predicate<IEffectConsumer> Predicate{ get; }
        public abstract int TargetNum{ get; }
    }
}