using System;
using System.Collections.Generic;
using Components.Damages;
using Components.Effects;
using Components.TileObjects.Effects;
using MVC;
using UnityEngine;
using Utility.Extensions;

namespace Components.Items.Instances{
    public class Hooklock: WeaponModel, IForceMovement, IDamage, IEffectTemplate{
        public Hooklock(){ ID = 1; }
        public override IEffectTemplate EffectTemplate => this;

        public override IEnumerable<Vector2Int> Range{ get; } = new[]{
            Vector2Int.right, new Vector2Int(2, 0), new Vector2Int(3, 0)
        };
        
        public override Predicate<IEffectConsumer> Predicate{ get; } = (controller)=>true;
        public override int TargetNum{ get; } = 1;
        public IEffectConsumer Target{ get; set; }
        public IController Source{ get; set; }
        public IEffect ToEffect() => this;
        public int Force{ get; } = 10;
        public Direction Direction{ get; set; }
        public bool Pulling{ get; } = true;

        public int Point{ set; get; } = 1;
        public ElementType Element{ get; } = ElementType.Physic;
    }
}