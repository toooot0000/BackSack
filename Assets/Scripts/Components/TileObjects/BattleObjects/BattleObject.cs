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

        public virtual IEffect Consume(IDamageEffect effect){
            // TODO update model!
            m_GetView().TakeDamage(effect.Damage);
            return null;
        }

        public virtual IEffect Consume(IBuffEffect buffEffect){
            buffEffect.Apply();
            if (buffEffect.ChangeNumber > 0){
                m_GetView().AddBuff(buffEffect.GetDisplayName());
            }
            return null;
        }

        private readonly List<IEffect> _results = new();
        public override IEffect Consume(IEffect effect){
            _results.Clear();
            var baseRet = base.Consume(effect);
            if(baseRet != null) _results.Add(base.Consume(effect));
            if(effect is IDamageEffect damageEffect) _results.Add(Consume(damageEffect));
            if(effect is IBuffEffect buffEffect) _results.Add(Consume(buffEffect));
            if (_results.Count == 0) return null;
            return new MultiEffect(_results.ToArray());
        }

        public List<Buff> Buffs{ get; set; } = new();
        
        private IBattleObjectModel m_GetModel() => Model as IBattleObjectModel;
        private IBattleObjectView m_GetView() => view as IBattleObjectView;
        
        
        
    }
}