using System;
using System.Collections.Generic;
using System.Linq;
using Components.Attacks;
using Components.Damages;
using Components.Effects;
using Components.Enemies.Intentions;
using Components.SelectMaps;
using Components.TileObjects;
using UnityEngine;

namespace Components.Enemies.Slimes{
    public class Attack: IEnemyIntention, IEnemyAttack{
        private readonly Enemy _enemy;
        public Attack(Enemy enemy){
            _enemy = enemy;
        }
        
        public IEffect DoAction(){
            EffectTemplate.Source = _enemy;
            return this.ToCoroutineEffect();
        }

        public void Label(SelectMap map){
            foreach (var targetPosition in TargetPositions){
                map.AddNewTile(new SelectMapTileOptions(targetPosition, Color.red));
            }
        }

        public IAttacker Attacker => _enemy;
        public Vector2Int[] TargetPositions{ get; private set; }

        public IEffectTemplate EffectTemplate{ get; } = new DamageEffect(null, null, new Damage(){
            Element = ElementType.Physic,
            Point = 1
        });
        public Predicate<ITileObject> TargetPredicate{ get; } = c => true;
        public int TargetNum{ get; } = 1;

        private ITileObject[] _targets = null;
        public IEnumerable<ITileObject> Targets => _targets ??= _enemy.SearchTargets(this).ToArray();

        public IAttackAnimator AttackAnimator;
        public IAttackAnimator GetAnimator() => AttackAnimator;

        private Vector2Int _dir;
        public Vector2Int Direction{
            set{
                _dir = value;
                TargetPositions = new[]{ value + _enemy.GetStagePosition() };
            }
            get => _dir;
        }
    }
}