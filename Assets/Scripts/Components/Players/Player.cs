using System;
using System.Linq;
using Components.Attacks;
using Components.Effects;
using Components.Items;
using Components.TileObjects.BattleObjects;
using UnityEngine;
using Utility.Extensions;

namespace Components.Players{
    public sealed class Player: BattleObject, IAttacker{
        public new PlayerView view;

        protected override void Awake(){
            base.Awake();
            base.view = view;
        }

        public new PlayerModel Model{
            set => SetModel(value);
            get => base.Model as PlayerModel;
        }

        protected override void AfterSetModel(){
            Model.CurrentStagePosition = stage.GridPositionToStagePosition(Vector2Int.zero);
            base.AfterSetModel();
        }

        public IEffect UseItem(DisposableModel disposableModel, Vector2Int position){
            throw new NotImplementedException();
        }

        public IEffect UseWeapon(WeaponModel weaponModel, Vector2Int direction){
            var attack = GetAttackWithWeapon(weaponModel, direction);
            return ProcessAttack(attack);
        }

        private IAttack GetAttackWithWeapon(WeaponModel item, Vector2Int direction){
            var rotatedRange = item.AttackRange.Select(v => {
                if (direction == ItemModel.DefaultDirection) return v;
                if (direction == ItemModel.DefaultDirection * -1) return -v;
                if (direction.IsClockwiseLess(ItemModel.DefaultDirection)) return v.Rotate90DegAntiClockwise();
                return v.Rotate90DegAntiClockwise();
            });
            item.EffectTemplate.Source = this;
            return new Attack(
                this, 
                GetStagePosition(), 
                rotatedRange.ToArray(), 
                item.EffectTemplate.ToEffect(), 
                item.Predicate, 
                item.TargetNum
            );
        }
    }
}