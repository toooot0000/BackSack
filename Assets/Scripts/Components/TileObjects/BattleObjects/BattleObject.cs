using System.Collections.Generic;
using Components.Attacks;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Damages;
using Components.Effects;
using Components.Grounds.Effects;
using Components.TileObjects.Effects;
using Components.TileObjects.ForceMovables;
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
        }

        public List<Buff> Buffs{ get; set; } = new();
        
        private IBattleObjectModel m_GetModel() => Model as IBattleObjectModel;
        
        private IBattleObjectView m_GetView() => view as IBattleObjectView;

        
    }
}