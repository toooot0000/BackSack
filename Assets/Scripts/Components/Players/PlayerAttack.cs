using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Components.Attacks;
using Components.Effects;
using Components.Grounds.Effects;
using Components.Items;
using Components.Stages;
using Components.TileObjects;
using Components.TileObjects.Effects;
using UnityEngine;
using Utility.Extensions;

namespace Components.Players{
    public class PlayerAttack : IAttack{
        public PlayerAttack(
            Player attacker,
            Vector2Int direction,
            Vector2Int[] targetPositions,
            ItemModel item
        ){
            _player = attacker;
            TargetPositions = targetPositions;
            EffectTemplate = item.EffectTemplate;
            TargetPredicate = item.Predicate;
            TargetNum = item.TargetNum;
            ItemModel = item;
            Direction = direction;
        }

        public readonly Vector2Int Direction;
        
        public readonly ItemModel ItemModel;

        private readonly Player _player;
        public IAttacker Attacker => _player;
        public Vector2Int[] TargetPositions{ get; }
        public IEffectTemplate EffectTemplate{ get; }
        public Predicate<ITileObject> TargetPredicate{ get; }
        public int TargetNum{ get; }
        private ITileObject[] _targets = null;
        public IEnumerable<ITileObject> Targets => _targets ??= _player.SearchTargets(this).ToArray();
    }
}