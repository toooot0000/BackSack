using System;
using System.Collections.Generic;
using Components.Attacks;
using Components.Effects;
using Components.Items.Animations;
using MVC;
using UnityEngine;
using Utility.Loader.Attributes;
using Object = UnityEngine.Object;

namespace Components.Items{
    [Table("items")]
    public abstract class ItemModel: SelfSetUpModel{
        
        public static readonly Vector2Int DefaultDirection = Vector2Int.right;
        public Vector2Int[] TakeUpRange;
        
        [Key("icon")]
        public string IconPath;
        public Vector2Int RotateDirection = Vector2Int.up;
        public abstract IEffectTemplate EffectTemplate{ get; }
        
        [Key("animator")]
        public string AnimatorPrefabPath;
        
        public abstract IEnumerable<Vector2Int> Range{ get; }
        public abstract Predicate<IEffectConsumer> Predicate{ get; }
        public virtual Func<IEffectConsumer, List<IEffectConsumer>> Reducer{ get; } = null;
        public abstract int TargetNum{ get; } // TargetNum == 0: 锁定目标范围内所有敌人
    }
}