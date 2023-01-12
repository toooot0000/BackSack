using System.Collections.Generic;
using Components.Attacks;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Buffs.Triggers;
using Components.Damages;
using Components.Effects;
using Components.Grounds.Effects;
using Components.TileObjects.Effects;
using Components.TileObjects.ForceMovables;
using MVC;
using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects.BattleObjects{
    public interface IBattleObjectModel: IForceMovableModel, IDamageableModel{ }
    
    public abstract class BattleObject: ForceMovable, IDamageable, IBuffHolder {
        public virtual IEffect Consume(IDamageEffect effect){
            // TODO update model!
            m_GetView().TakeDamage(effect.Damage);
            return null;
        }

        public virtual IEffect Die(){
            m_GetView().Die();
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
            
            AddTypedEffectConsumer<IEffect>(_results, effect, base.Consume);
            AddTypedEffectConsumer<IBuffEffect>(_results, effect, this);
            AddTypedEffectConsumer<IDamageEffect>(_results, effect, this);
            return MakeSideEffect(_results);

            // return _results.Count switch{
            //     0 => null,
            //     1 => _results[0],
            //     _ => new MultiEffect(_results.ToArray())
            // };
        }

        public List<Buff> Buffs{ get; set; } = new();
        
        private IBattleObjectModel m_GetModel() => Model as IBattleObjectModel;
        
        private IBattleObjectView m_GetView() => view as IBattleObjectView;
        
        
        /// <summary>
        /// Apply attack and return side effects
        /// </summary>
        /// <param name="attack"></param>
        /// <returns></returns>
        protected IEffect ProcessAttack(IAttack attack){
            var effects = new List<IEffect>();
            var curTargetNum = 0;
            var forceMovement = attack.Effect as IForceMovement;
            foreach (var relPos in attack.RelativeRange){
                var stagePos = relPos + attack.AttackerPosition;
                IEffect side;
                if(attack.Effect is IGroundEffect groundEffect){
                    side = stage.Consume(groundEffect, stagePos);
                    if(side != null) effects.Add(side);
                } 
                
                var target = stage.GetTileObject(stagePos);
                if (target == null || !attack.Predicate(target)) continue;
                if(forceMovement != null){
                    if (forceMovement.Pulling)
                        forceMovement.Direction =
                            (m_GetModel().CurrentStagePosition - target.GetStagePosition()).ToDirection();
                    else
                        forceMovement.Direction =
                            (target.GetStagePosition() - m_GetModel().CurrentStagePosition).ToDirection();
                }
                if (attack.TargetNum > 0 && curTargetNum >= attack.TargetNum) break;
                side = target.Consume(attack.Effect);
                curTargetNum++;
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