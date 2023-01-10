using System;
using System.Collections.Generic;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Damages;
using Components.Effects;
using Components.TileObjects;
using Components.TileObjects.ForceMovable;
using UnityEngine;

namespace Components.Players{
    public sealed class Player: ForceMovable, IDamageable, IBuffHolder{
        public PlayerView view;
        private new PlayerModel Model{
            set => SetModel(value);
            get => base.Model as PlayerModel;
        }

        protected override void AfterSetModel(){
            Model.CurrentStagePosition = stage.GridPositionToStagePosition(Vector2Int.zero);
            view.SetPosition(stage.GridPositionToWorldPosition(Vector2Int.zero));
        }

        public IEffectResult Consume(IDamageEffect effect){
            view.TakeDamage(effect.Damage);
            return new EffectResult(effect, this);
        }

        public IEffectResult Consume(IBuffEffect buffEffect){
            buffEffect.Apply();
            if (buffEffect.ChangeNumber > 0){
                view.AddBuff(buffEffect.GetDisplayName());
            }
            return new EffectResult(buffEffect, this);
        }

        private readonly List<IEffectResult> _results = new();
        public override IEffectResult[] Consume(IEffect effect){
            _results.Clear();
            _results.AddRange(base.Consume(effect));
            if (effect is IMultiEffect multiEffect){
                foreach (var subEffect in multiEffect.Effects){
                    Consume(subEffect);
                }
            }
            if(effect is IDamageEffect damageEffect) _results.Add(Consume(damageEffect));
            if(effect is IBuffEffect buffEffect) _results.Add(Consume(buffEffect));
            return _results.ToArray();
        }

        protected override ITileObjectModel GetModel() => Model;

        protected override ITileObjectView GetView() => view;
        public List<Buff> Buffs{ get; set; } = new();
    }
}