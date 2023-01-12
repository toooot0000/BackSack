using System.Collections.Generic;
using Components.Attacks;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Buffs.Triggers;
using Components.Damages;
using Components.Effects;
using Components.Grounds.Effects;
using Components.TileObjects.ForceMovables;
using MVC;
using UnityEngine;

namespace Components.TileObjects.BattleObjects{
    public interface IBattleObjectModel: IForceMovableModel, IDamageableModel{ }
    
    public abstract class BattleObject: ForceMovable, IDamageable, IBuffHolder {
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
            if (_results.Count == 1) return _results[0];
            return new MultiEffect(_results.ToArray());
        }

        public List<Buff> Buffs{ get; set; } = new();
        
        private IBattleObjectModel m_GetModel() => Model as IBattleObjectModel;
        
        private IBattleObjectView m_GetView() => view as IBattleObjectView;

        protected IEffect ProcessAttack(IAttack attack){
            var effects = new List<IEffect>();
            foreach (var relPos in attack.RelativeRange){
                var stagePos = relPos + attack.Attacker.GetStagePosition();
                IEffect side;
                if(attack.Effect is IGroundEffect groundEffect){
                    side = stage.Consume(groundEffect, stagePos);
                    if(side != null) effects.Add(side);
                } 
                var target = stage.GetTileObject(stagePos);
                if (target == null || !attack.Predicate(target)) continue;
                side = target.Consume(attack.Effect);
                if (side != null) effects.Add(side);
            }
            return effects.Count switch{
                0 => null,
                1 => effects[0],
                _ => new MultiEffect(effects.ToArray())
            };
        }
    }
}