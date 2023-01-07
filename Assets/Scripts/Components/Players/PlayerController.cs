using System;
using System.Collections.Generic;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Damages;
using Components.Effects;
using Components.Stages;
using Components.TileObjects;
using Components.TileObjects.Effects;
using Components.TileObjects.ForceMovable;
using MVC;
using UnityEngine;

namespace Components.Players{
    public class PlayerController: ForceMovableController, IDamageable, IBuffHolder{
        public PlayerView view;
        private new Player Model{
            set => SetModel(value);
            get => base.Model as Player;
        }

        protected override void AfterSetModel(){
            Model.CurrentStagePosition = stageController.GridPositionToStagePosition(Vector2Int.zero);
            view.SetPosition(stageController.GridPositionToWorldPosition(Vector2Int.zero));
        }

        /// <summary>
        /// Pure Test
        /// </summary>
        public void Jump(){
            view.Jump();
        }

        public IEffectResult Consume(IDamageEffect effect){
            view.TakeDamage(effect.Damage);
            return new EffectResult(effect, this);
        }

        public IEffectResult Consume(IBuffEffect buffEffect){
            if (buffEffect.ChangeNumber > 0){
                view.AddBuff(buffEffect.GetDisplayName());
            } else{
                view.RemoveBuff(buffEffect.GetDisplayName());
            }
            return new EffectResult(buffEffect, this);
        }

        private List<IEffectResult> _results;
        public override IEffectResult[] Consume(IEffect effect){
            _results.Clear();
            _results.AddRange(base.Consume(effect));
            if(effect is IDamageEffect damageEffect) _results.Add(Consume(damageEffect));
            if(effect is IBuffEffect buffEffect) _results.Add(Consume(buffEffect));
            return _results.ToArray();
        }

        protected override ITileObjectModel GetModel() => Model;

        protected override ITileObjectView GetView() => view;
    }
}