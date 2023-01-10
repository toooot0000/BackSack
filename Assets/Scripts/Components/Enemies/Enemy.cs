using System.Collections.Generic;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Damages;
using Components.Effects;
using Components.TileObjects;
using Components.TileObjects.ForceMovable;
using UnityEngine;

namespace Components.Enemies{
    public class Enemy: ForceMovable, IDamageable, IBuffHolder{
        public EnemyView view;

        public new EnemyModel Model{
            set => SetModel(value);
            get => base.Model as EnemyModel;
        }

        protected override void AfterSetModel(){
            var sprite = Resources.Load<Sprite>(Model.GetSpriteResourcePath());
            if (sprite != null) view.SetSprite(sprite);
            view.SetPosition(stage.StagePositionToWorldPosition(Model.CurrentStagePosition));
        }

        protected override ITileObjectModel GetModel() => Model;
        protected override ITileObjectView GetView() => view;

        public IEffectResult Consume(IDamageEffect effect){
            throw new System.NotImplementedException();
        }

        public IEffectResult Consume(IBuffEffect effect){
            throw new System.NotImplementedException();
        }

        private readonly List<IEffectResult> _results = new();
        public override IEffectResult[] Consume(IEffect effect){
            _results.Clear();
            _results.AddRange(base.Consume(effect));
            if(effect is IDamageEffect damageEffect) _results.Add(Consume(damageEffect));
            if(effect is IBuffEffect buffEffect) _results.Add(Consume(buffEffect));
            return _results.ToArray();
        }

        public List<Buff> Buffs{ get; set; } = new();
    }
}