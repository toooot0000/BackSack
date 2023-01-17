using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Components.Attacks;
using Components.Effects;
using Components.Items;
using Components.Items.Animations;
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

#region Attack

        public IEffect UseItem(DisposableModel disposableModel, Vector2Int position){
            throw new NotImplementedException();
        }

        public CoroutineEffect UseWeapon(WeaponModel weaponModel, Vector2Int direction){
            var attack = GetAttackWithWeapon(weaponModel, direction);
            if (!attack.Targets.Any()) return null;
            return attack.ToCoroutineEffect();
        }

        private PlayerAttack GetAttackWithWeapon(WeaponModel item, Vector2Int direction){
            var rotatedRange = item.Range.Select(v => {
                if (direction == ItemModel.DefaultDirection) return v;
                if (direction == ItemModel.DefaultDirection * -1) return -v;
                if (direction.IsClockwiseLess(ItemModel.DefaultDirection)) return v.Rotate90DegAntiClockwise();
                return v.Rotate90DegAntiClockwise();
            }).Select(v => v + GetStagePosition());
            return new PlayerAttack(
                this,
                direction,
                rotatedRange.ToArray(), 
                item
            );
        }

        private readonly Dictionary<int, IAttackAnimator> _animators = new();
        public IAttackAnimator GetAttackAnimator(IAttack attack){
            if (attack is not PlayerAttack pAtt) return null;
            var id = pAtt.ItemModel.ID!.Value;
            if (_animators.ContainsKey(id)){
                return _animators[id];
            }
            var prefab = Resources.Load<GameObject>(pAtt.ItemModel.AnimatorPrefabPath);
            if (prefab == null) return null;
            var ret = Instantiate(prefab, transform).GetComponent<ItemAnimator>();
            _animators[id] = ret;
            return ret;
        }
        

#endregion


        
    }
}