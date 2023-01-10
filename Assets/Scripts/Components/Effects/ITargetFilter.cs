using System;
using System.Collections.Generic;
using MVC;
using UnityEngine;

namespace Components.Effects{
    public interface ITargetFilter{
        Rect Range{ get; }
        Predicate<IController> Predicate{ get; }
    }

    public class TargetFilter: ITargetFilter{
        public TargetFilter(Rect range, Predicate<IController> predicate){
            Range = range;
            Predicate = predicate;
        }
        public Rect Range{ get; }
        public Predicate<IController> Predicate{ get; }
    }
}