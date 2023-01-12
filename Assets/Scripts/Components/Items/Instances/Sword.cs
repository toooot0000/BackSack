using System;
using System.Collections.Generic;
using Components.Damages;
using Components.Effects;
using Components.Grounds;
using Components.Grounds.Effects;
using MVC;
using UnityEngine;

namespace Components.Items.Instances{
    public class Sword: WeaponModel{
        private class Eff : IDamageEffect, ICreateNewGround{
            public ElementType Element{ get; } = ElementType.Water;
            public bool Create{ get; } = true;
            public int LastTurnNum{ get; } = 10;
            public IEffectConsumer Target{ set; get; } = null;
            public IController Source{ set; get; } = null;

            public Damage Damage{ set; get; } = new Damage(){
                Point = 1,
                Element = ElementType.Water
            };

            public GroundType GroundType{ get; } = GroundType.Water;
        }
        
        public override IEnumerable<Vector2Int> AttackRange{ get; } =
            new[]{ Vector2Int.right, new Vector2Int(1, 1), new Vector2Int(1, -1) };

        public override IEffect Effect{ get; } = new Eff();
        public override Predicate<IEffectConsumer> Predicate{ get; } = controller => true;
        public override int LastTurn{ get; } = 10;
    }
}