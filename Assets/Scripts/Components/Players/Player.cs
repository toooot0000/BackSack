using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Components.Attacks;
using Components.BackPacks;
using Components.Effects;
using Components.Grounds.Effects;
using Components.Items;
using Components.Items.Animations;
using Components.TileObjects;
using Components.TileObjects.BattleObjects;
using UnityEngine;
using Utility.Extensions;

namespace Components.Players{
    public sealed class Player: BattleObject, IAttacker{
        public PlayerView view;
        public BackPack backPack;

        protected override void Awake(){
            base.Awake();
            backPack = GetComponent<BackPack>();
        }

        private PlayerModel _model;
        public PlayerModel Model{
            set{
                _model = value;
                _model.CurrentStagePosition = stage.GridToStagePosition(Vector2Int.zero);
                SetStagePosition(_model.CurrentStagePosition);
            }
            get => _model;
        }

        public override ITileObjectView View => view;

        #region Attack

        public IEffect UseItemWithPosition(DisposableModel disposableModel, Vector2Int position){
            throw new NotImplementedException();
        }

        public CoroutineEffect UseItemWithDirection(ItemModel weaponModel, Direction direction){
            var attack = GetAttackWithWeapon(weaponModel, direction);
            if (!attack.Targets.Any() && attack.EffectTemplate is not ICreateNewGround) return null;
            return attack.ToCoroutineEffect();
        }

        private PlayerAttack GetAttackWithWeapon(ItemModel item, Direction direction){
            var rotatedRange = item.Range.Rotate(direction).Select(v => v + GetStagePosition());
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

        public override Vector2Int CurrentStagePosition{
            set => Model.CurrentStagePosition = value;
            get => Model.CurrentStagePosition;
        }
        
        public override int Weight{ 
            get => Model.Weight;
            set => Model.Weight = value;
        }

        public override int HealthLimit{
            get => Model.HealthLimit;
            set => Model.HealthLimit = value;
        }
        public override int HealthPoint{
            get => Model.HealthPoint;
            set => Model.HealthPoint = value;
        }
        public override int ShieldPoint{ 
            get => Model.ShieldPoint;
            set => Model.ShieldPoint = value;
        }
        public override int DefendPoint{
            get => Model.DefendPoint;
            set => Model.DefendPoint = value;
        }
    }
}