using System;
using System.Collections.Generic;
using System.Linq;
using Components.Attacks;
using Components.Effects;
using Components.Items.Animations;
using MVC;
using UnityEngine;
using Utility.Extensions;
using Utility.Loader.Attributes;
using Object = UnityEngine.Object;

namespace Components.Items{
    [Table("items")]
    public abstract class ItemModel: SelfSetUpModel{
        public Vector2Int[] TakeUpRange;
        
        [Key("icon")]
        public string IconPath;
        public abstract IEffectTemplate EffectTemplate{ get; }
        
        [Key("animator")]
        public string AnimatorPrefabPath;
        
        public abstract IEnumerable<Vector2Int> Range{ get; }
        public abstract Predicate<IEffectConsumer> Predicate{ get; }
        public virtual Func<IEffectConsumer, List<IEffectConsumer>> Reducer{ get; } = null;
        public abstract int TargetNum{ get; } // TargetNum == 0: 锁定目标范围内所有敌人
    }


    public static class EnumerableExtensions{

        public static IEnumerable<Vector2Int> Rotate(this IEnumerable<Vector2Int> src, Direction direction){
            
            if (direction == Const.DefaultDirection){
                return src;
            }
            if(direction == Const.DefaultDirection.Opposite()){
                return src.Select(v => -v);
            }
            if (direction.IsClockwiseLess(Const.DefaultDirection)){
                return src.Select(v => v.Rotate90DegAntiClockwise());
            }
            return src.Select(v => v.Rotate90DegClockwise());
        }
    }
}