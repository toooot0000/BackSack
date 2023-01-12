using System;
using System.Collections.Generic;
using Components.Effects;
using Components.TileObjects.Effects;
using MVC;
using UnityEngine;

namespace Components.Items.Instances{
    public class HookLock: WeaponModel, IForceMovement{
        public override IEffect Effect => this;

        public override IEnumerable<Vector2Int> AttackRange{ get; } = new[]{
            Vector2Int.right, new Vector2Int(2, 0), new Vector2Int(3, 0)
        };
        
        public override Predicate<IEffectConsumer> Predicate{ get; } = (controller)=>true;
        public override int TargetNum{ get; } = 1;
        public IEffectConsumer Target{ get; set; }
        public IController Source{ get; set; }
        public int Force{ get; } = 10;
        public Vector2Int Direction{ get; set; }
        public bool Pulling{ get; } = false;
    }
}