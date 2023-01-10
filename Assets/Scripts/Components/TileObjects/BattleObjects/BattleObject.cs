using System.Collections.Generic;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Buffs.Triggers;
using Components.Damages;
using Components.Effects;
using Components.TileObjects.ForceMovables;
using UnityEngine;

namespace Components.TileObjects.BattleObjects{
    public interface IBattleObjectModel: IForceMovableModel, IDamageableModel{ }
    
    public abstract class BattleObject: ForceMovable, IDamageable, IBuffHolder {

        public virtual IEffect OnTurnStarts(){
            return null;
        }

        public virtual IEffect OnTurnEnds(){
            return null;
        }

        public virtual IEffectResult Consume(IDamageEffect effect){
            // TODO update model!
            m_GetView().TakeDamage(effect.Damage);
            return new EffectResult(effect, this);
        }

        public virtual IEffectResult Consume(IBuffEffect buffEffect){
            buffEffect.Apply();
            if (buffEffect.ChangeNumber > 0){
                m_GetView().AddBuff(buffEffect.GetDisplayName());
            }
            return new EffectResult(buffEffect, this);
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
        
        private IBattleObjectModel m_GetModel() => Model as IBattleObjectModel;
        private IBattleObjectView m_GetView() => view as IBattleObjectView;
        
        
        
    }
}