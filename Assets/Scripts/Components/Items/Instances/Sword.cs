using System;
using System.Collections.Generic;
using Components.Damages;
using Components.Effects;
using Components.Grounds;
using Components.Grounds.Effects;
using MVC;
using UnityEngine;
using Utility.Loader;
using Utility.Loader.Attributes;

namespace Components.Items.Instances{
    public class Sword: WeaponModel{
        private class Eff : IEffectTemplate, IDamageEffect, ICreateNewGround{
            public ElementType Element{ get; } = ElementType.Water;
            public bool Create{ get; } = true;
            public int LastTurnNum{ get; } = 10;
            public IEffectConsumer Target{ set; get; } = null;
            public IController Source{ set; get; } = null;
            public IEffect ToEffect() => this;

            public Damage Damage{ set; get; } = new Damage(){
                Point = 1,
                Element = ElementType.Water
            };

            public GroundType GroundType{ get; } = GroundType.Water;
        }

        public Sword(){ ID = 1; }
        
        public override IEnumerable<Vector2Int> Range{ get; } =
            new[]{ Vector2Int.right, new Vector2Int(1, 1), new Vector2Int(1, -1) };

        public override IEffectTemplate EffectTemplate{ get; } = new Eff();
        public override Predicate<IEffectConsumer> Predicate{ get; } = controller => true;
        public override int TargetNum{ get; } = 0;
    }
}