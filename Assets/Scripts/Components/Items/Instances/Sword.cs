using System;
using System.Collections.Generic;
using Components.Damages;
using Components.Effects;
using MVC;
using UnityEngine;

namespace Components.Items.Instances{
    public class Sword: ItemModel{
        private class Eff : IDamageEffect{
            public ElementType Element{ get; } = ElementType.Physic;
            public IController Target{ set; get; } = null;
            public IController Source{ set; get; } = null;

            public Damage Damage{ set; get; } = new Damage(){
                Point = 1,
                Type = ElementType.Water
            };
        }
        
        public override IEnumerable<Vector2Int> AttackRange{ get; } =
            new[]{ Vector2Int.right, new Vector2Int(1, 1), new Vector2Int(1, -1) };

        public override IEffect Effect{ get; } = new Eff();
        public override Predicate<IController> Predicate{ get; } = controller => true;
        public override int LastTurn{ get; } = 10;
    }
}